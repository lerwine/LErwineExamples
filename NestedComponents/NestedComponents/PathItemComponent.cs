using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace NestedComponents
{
    /// <summary>
    /// Base class for nested, path-based, self-named components
    /// </summary>
    /// <typeparam name="TPathItemComponent">Type of class inheriting from this base class.</typeparam>
    public abstract class PathItemComponent<TPathItemComponent> : IComponent, INestedContainer, IList<TPathItemComponent>, IEquatable<TPathItemComponent>
        where TPathItemComponent : PathItemComponent<TPathItemComponent>
    {
        class NestedSite<TNestedComponent> : INestedSite
            where TNestedComponent : PathItemComponent<TNestedComponent>
        {
            //
            // Summary:
            //     Gets or sets the name of the component associated with the System.ComponentModel.ISite
            //     when implemented by a class.
            //
            // Returns:
            //     The name of the component associated with the System.ComponentModel.ISite;
            //     or null, if no name is assigned to the component.
            public string Name { get { return this.Component.Name; } }

            string ISite.Name
            {
                get { return this.Name; }
                set { throw new NotSupportedException(); }
            }

            // Summary:
            //     Gets the full name of the component in this site.
            //
            // Returns:
            //     The full name of the component in this site.
            public string FullName { get { return this.Component.FullName; } }

            // Summary:
            //     Gets the component associated with the System.ComponentModel.ISite when implemented
            //     by a class.
            //
            // Returns:
            //     The System.ComponentModel.IComponent instance associated with the System.ComponentModel.ISite.
            internal TNestedComponent Component { get; private set; }

            IComponent ISite.Component { get { return this.Component; } }

            //
            // Summary:
            //     Gets the System.ComponentModel.IContainer associated with the System.ComponentModel.ISite
            //     when implemented by a class.
            //
            // Returns:
            //     The System.ComponentModel.IContainer instance associated with the System.ComponentModel.ISite.
            internal TNestedComponent Container { get; private set; }

            IContainer ISite.Container { get { return this.Container; } }

            //
            // Summary:
            //     Determines whether the component is in design mode when implemented by a
            //     class.
            //
            // Returns:
            //     true if the component is in design mode; otherwise, false.
            public bool DesignMode { get { return false; } }

            internal NestedSite(TNestedComponent component, TNestedComponent container)
            {
                this.Component = component;
                this.Container = container;
            }

            /// <summary>
            /// Gets the service object of the specified type.
            /// </summary>
            /// <param name="service">An object that specifies the type of service object to get.</param>
            /// <returns> service object of type serviceType.-or- null if there is no service object
            /// of type serviceType.</returns>
            public Object GetService(Type service) { return ((service == typeof(ISite)) ? this : this.Container.GetService(service)); }
        }

        public event EventHandler Disposed;

        private string _name = "";
        private NestedSite<TPathItemComponent> _site;
        private List<TPathItemComponent> _components = new List<TPathItemComponent>();

        public string Name { get; private set; }

        public string FullName
        {
            get
            {
                return String.Join("/", this.GetAncestors(true).Select(a => Uri.EscapeDataString(a.Name)).ToArray());
            }
        }

        // Summary:
        //     Gets or sets the System.ComponentModel.ISite associated with the System.ComponentModel.IComponent.
        //
        // Returns:
        //     The System.ComponentModel.ISite object associated with the component; or
        //     null, if the component does not have a site.
        private NestedSite<TPathItemComponent> Site
        {
            get { return this._site; }
            set
            {
                lock (this._components)
                {
                    if (this._site != null)
                        this._site.Container.Disposed -= new EventHandler(this.OnContainerDisposed);

                    if (value != null)
                        value.Container.Disposed += new EventHandler(this.OnContainerDisposed);

                    this._site = value;
                }
            }
        }

        ISite IComponent.Site
        {
            get { return this.Site; }
            set { throw new NotSupportedException(); }
        }

        public int Count { get { return this._components.Count; } }

        bool ICollection<TPathItemComponent>.IsReadOnly { get { return false; } }

        public TPathItemComponent Container
        {
            get
            {
                NestedSite<TPathItemComponent> s = this.Site;
                return s == null ? null : s.Container;
            }
        }

        IComponent INestedContainer.Owner { get { return this.Container; } }

        protected bool DesignMode
        {
            get
            {
                NestedSite<TPathItemComponent> s = this.Site;
                return (s == null) ? false : s.DesignMode;
            }
        }

        ComponentCollection IContainer.Components
        {
            get
            {
                ComponentCollection result;

                lock (this._components)
                    result = new ComponentCollection(this._components.ToArray());

                return result;
            }
        }

        public TPathItemComponent this[int index]
        {
            get { return this._components[index]; }
            set
            {
                if (index == this._components.Count)
                    this.Add(value);

                if (index > this._components.Count || index < 0)
                    throw new IndexOutOfRangeException();

                if (value == null)
                    throw new ArgumentNullException();

                TPathItemComponent toRemove = this._components[index];
                if (Object.ReferenceEquals(toRemove, value))
                    return;

                NestedSite<TPathItemComponent> site = value.Site;

                lock (this._components)
                {
                    value.Site = this.CreateSite(value, "value", index);
                    this._components[index] = value;
                    toRemove.Site = null;
                }
            }
        }

        public PathItemComponent(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (name.Length == 0)
                throw new ArgumentException("Name cannot be empty", "name");

            this.Name = name;
        }

        protected PathItemComponent(string name, TPathItemComponent container) 
            : this(name)
        {
            if (container != null)
                container._Add(this as TPathItemComponent, "name");
        }

        ~PathItemComponent()
        {
            this.Dispose(false);
        }

        public void Clear()
        {
            lock (this._components)
            {
                foreach (TPathItemComponent component in this._components)
                    component.Site = null;
                this._components.Clear();
            }
        }

        public int IndexOf(TPathItemComponent item) { return this._components.IndexOf(item); }

        public bool Contains(TPathItemComponent item) { return this._components.Contains(item); }

        public void Add(TPathItemComponent component)
        {
            this._Add(component, "component");
        }

        private void _Add(TPathItemComponent component, string paramName)
        {
            lock (this._components)
            {
                if (component == null)
                    return;

                component.Site = this.CreateSite(component, paramName);
                this._components.Add(component);
            }
        }

        void IContainer.Add(IComponent component, string name)
        {
            if (component == null)
                throw new ArgumentNullException("component");

            TPathItemComponent nestedComponent = (TPathItemComponent)component;
            if (name != null && String.Compare(nestedComponent.Name, name, true) == 0)
                throw new ArgumentException("Mame does not match component name.");

            this.Add(nestedComponent);
        }

        void IContainer.Add(IComponent component) { this.Add((TPathItemComponent)component); }

        public void Insert(int index, TPathItemComponent item)
        {
            lock (this._components)
            {
                if (item == null)
                    return;

                item.Site = this.CreateSite(item, "item");
                this._components.Insert(index, item);
            }
        }

        public bool Remove(TPathItemComponent component) { return this.Remove(component, false); }

        void IContainer.Remove(IComponent component) { this.Remove((TPathItemComponent)component, false); }

        private bool Remove(TPathItemComponent component, bool preserveSite)
        {
            if (component == null)
                return false;

            bool result;

            lock (this._components)
            {
                NestedSite<TPathItemComponent> site = component.Site;
                if (site != null && Object.ReferenceEquals(site.Container, this))
                {
                    if (!preserveSite)
                        component.Site = null;
                    result = this._components.Remove(component);
                }
                else
                    result = false;
            }

            return result;
        }

        protected void RemoveWithoutUnsiting(TPathItemComponent component)
        {
            this.Remove(component, true);
        }

        public void RemoveAt(int index)
        {
            lock (this._components)
            {
                TPathItemComponent component = this._components[index];

                NestedSite<TPathItemComponent> site = component.Site;
                if (site != null && Object.ReferenceEquals(site.Container, this))
                {
                    component.Site = null;
                    this._components.RemoveAt(index);
                }
            }
        }

        /// <summary>
        /// This should only be called when thread is blocked using this._components.
        /// </summary>
        /// <param name="component">Component to be added.</param>
        /// <param name="paramName">Name of parameter responsible for adding component.</param>
        /// <param name="ignoreIndex">Index of item being replaced.</param>
        /// <returns>A Site object which can be used in the component.</returns>
        protected NestedSite<TPathItemComponent> CreateSite(TPathItemComponent component, string paramName, int ignoreIndex = -1)
        {
            if (component == null)
                throw new ArgumentNullException("component");

            NestedSite<TPathItemComponent> site = component.Site;

            for (int i = 0; i < this._components.Count; i++)
            {
                if (i != ignoreIndex && String.Compare(this._components[i].Name, component.Name, true) == 0)
                    throw new ArgumentOutOfRangeException(paramName, "Duplicate names cannot be added to the same container.");
            }

            if (Object.ReferenceEquals(component, this))
                throw new ArgumentOutOfRangeException(paramName, "Component cannot contain itself.");

            if (this.GetDescendants(false).Any(a => Object.ReferenceEquals(a, component)))
                throw new ArgumentOutOfRangeException(paramName, "Descendant components cannot be added to their own ancestors.");

            if (this.GetAncestors(false).Any(a => Object.ReferenceEquals(a, component)))
                throw new ArgumentOutOfRangeException(paramName, "Ancestor components cannot be added to their own descendants.");

            if (site != null && !Object.ReferenceEquals(site.Container, this))
                site.Container.Remove(component);

            return new NestedSite<TPathItemComponent>(component, (TPathItemComponent)this);
        }

        [Obsolete("Use method which passes paramName", false)]
        protected virtual NestedSite<TPathItemComponent> CreateSite(TPathItemComponent component)
        {
            if (component == null)
                throw new ArgumentNullException("component");

            return new NestedSite<TPathItemComponent>(component, (TPathItemComponent)this);
        }

        protected internal virtual object GetService(Type service)
        {
            NestedSite<TPathItemComponent> s = this.Site;
            return ((s == null) ? ((service == typeof(IContainer) || service == typeof(INestedContainer) || service == typeof(TPathItemComponent)) ? this : null) : s.GetService(service));
        }

        private void OnContainerDisposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public virtual bool Equals(TPathItemComponent other)
        {
            return other != null && (Object.ReferenceEquals(this, other) || this.FullName == other.FullName);
        }

        public override bool Equals(object obj) { return base.Equals(obj); }

        public override string ToString() { return this.FullName; }

        public override int GetHashCode() { return this.FullName.GetHashCode(); }

        public void CopyTo(TPathItemComponent[] array, int arrayIndex) { this._components.CopyTo(array, arrayIndex); }

        public IEnumerator<TPathItemComponent> GetEnumerator() { return this._components.GetEnumerator(); }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return (this._components as System.Collections.IEnumerable).GetEnumerator(); }

        public IEnumerable<TPathItemComponent> GetAncestors(bool includeSelf)
        {
            NestedSite<TPathItemComponent> s = this.Site;

            if (s == null)
                return (includeSelf) ? new TPathItemComponent[] { this as TPathItemComponent } : new TPathItemComponent[0];

            return (includeSelf) ? s.Container.GetAncestors(true).Concat(new TPathItemComponent[] { this as TPathItemComponent }) : s.Container.GetAncestors(true);
        }

        public IEnumerable<TPathItemComponent> GetDescendants(bool includeSelf)
        {
            IEnumerable<TPathItemComponent> descendants = this._components.SelectMany(s => s.GetDescendants(true));

            return (includeSelf) ? (new TPathItemComponent[] { this as TPathItemComponent }).Concat(descendants) : descendants;
        }

        // Summary:
        //     Represents the method that handles the System.ComponentModel.IComponent.Disposed
        //     event of a component.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            lock (this._components)
            {
                if (this.Site != null)
                {
                    this.Site.Container.Disposed -= new EventHandler(this.OnContainerDisposed);
                    this.Site.Container.Remove((TPathItemComponent)this);
                }
            }

            if (this.Disposed != null)
                this.Disposed(this, EventArgs.Empty);
        }
    }

    public class PathItemComponent : PathItemComponent<PathItemComponent>
    {

    }
}
