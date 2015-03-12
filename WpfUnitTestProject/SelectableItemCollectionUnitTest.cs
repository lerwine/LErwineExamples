using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LennysWpfLibrary.Collections;

namespace WpfUnitTestProject
{
    [TestClass]
    public class SelectableItemCollectionUnitTest
    {
        [TestMethod]
        public void ConstructorTestMethod1()
        {
            SelectableItemCollection<SelectableCollectionItem<int>> collection = new SelectableItemCollection<SelectableCollectionItem<int>>();
            Assert.IsFalse(collection.MultiSelect);
            int expected = 0;
            int actual = collection.Count;
            Assert.AreEqual(expected, actual);
            actual = collection.SelectedItems.Count;
            Assert.AreEqual(expected, actual);
            expected = -1;
            actual = collection.SelectedIndex;
            Assert.AreEqual(expected, actual);
            Assert.IsNull(collection.SelectedItem);
        }

        [TestMethod]
        public void ConstructorTestMethod2()
        {
            SelectableCollectionItem<int>[] items = new SelectableCollectionItem<int>[0];
            SelectableItemCollection<SelectableCollectionItem<int>> collection = new SelectableItemCollection<SelectableCollectionItem<int>>(items);
            int expected = 0;
            int actual = collection.Count;
            Assert.AreEqual(expected, actual);
            actual = collection.SelectedItems.Count;
            Assert.AreEqual(expected, actual);
            expected = -1;
            actual = collection.SelectedIndex;
            Assert.AreEqual(expected, actual);
            Assert.IsNull(collection.SelectedItem);

            items = new SelectableCollectionItem<int>[]
            {
                new SelectableCollectionItem<int>(5, false),
                new SelectableCollectionItem<int>(6, false)
            };

            collection = new SelectableItemCollection<SelectableCollectionItem<int>>(items);
            expected = 2;
            actual = collection.Count;
            Assert.AreEqual(expected, actual);
            expected = 0;
            actual = collection.SelectedItems.Count;
            Assert.AreEqual(expected, actual);
            expected = -1;
            actual = collection.SelectedIndex;
            Assert.AreEqual(expected, actual);
            Assert.IsNull(collection.SelectedItem);

            items = new SelectableCollectionItem<int>[]
            {
                new SelectableCollectionItem<int>(7, true),
                new SelectableCollectionItem<int>(6, false)
            };

            collection = new SelectableItemCollection<SelectableCollectionItem<int>>(items);
            expected = 2;
            actual = collection.Count;
            Assert.AreEqual(expected, actual);
            expected = 1;
            actual = collection.SelectedItems.Count;
            Assert.AreEqual(expected, actual);
            expected = 0;
            actual = collection.SelectedIndex;
            Assert.AreEqual(expected, actual);
            Assert.IsNotNull(collection.SelectedItem);
            Assert.AreSame(items[0], collection.SelectedItem);

            items = new SelectableCollectionItem<int>[]
            {
                new SelectableCollectionItem<int>(6, false),
                new SelectableCollectionItem<int>(12, true)
            };

            collection = new SelectableItemCollection<SelectableCollectionItem<int>>(items);
            expected = 2;
            actual = collection.Count;
            Assert.AreEqual(expected, actual);
            expected = 1;
            actual = collection.SelectedItems.Count;
            Assert.AreEqual(expected, actual);
            expected = 1;
            actual = collection.SelectedIndex;
            Assert.AreEqual(expected, actual);
            Assert.IsNotNull(collection.SelectedItem);
            Assert.AreSame(items[1], collection.SelectedItem);
        }

        [TestMethod]
        public void ConstructorTestMethod3()
        {
            SelectableCollectionItem<int>[] items = new SelectableCollectionItem<int>[0];
            SelectableItemCollection<SelectableCollectionItem<int>> collection = new SelectableItemCollection<SelectableCollectionItem<int>>(items.ToList());
            int expected = 0;
            int actual = collection.Count;
            Assert.AreEqual(expected, actual);
            actual = collection.SelectedItems.Count;
            Assert.AreEqual(expected, actual);
            expected = -1;
            actual = collection.SelectedIndex;
            Assert.AreEqual(expected, actual);
            Assert.IsNull(collection.SelectedItem);

            items = new SelectableCollectionItem<int>[]
            {
                new SelectableCollectionItem<int>(5, false),
                new SelectableCollectionItem<int>(6, false)
            };

            collection = new SelectableItemCollection<SelectableCollectionItem<int>>(items.ToList());
            expected = 2;
            actual = collection.Count;
            Assert.AreEqual(expected, actual);
            expected = 0;
            actual = collection.SelectedItems.Count;
            Assert.AreEqual(expected, actual);
            expected = -1;
            actual = collection.SelectedIndex;
            Assert.AreEqual(expected, actual);
            Assert.IsNull(collection.SelectedItem);

            items = new SelectableCollectionItem<int>[]
            {
                new SelectableCollectionItem<int>(7, true),
                new SelectableCollectionItem<int>(6, false)
            };

            collection = new SelectableItemCollection<SelectableCollectionItem<int>>(items.ToList());
            expected = 2;
            actual = collection.Count;
            Assert.AreEqual(expected, actual);
            expected = 1;
            actual = collection.SelectedItems.Count;
            Assert.AreEqual(expected, actual);
            expected = 0;
            actual = collection.SelectedIndex;
            Assert.AreEqual(expected, actual);
            Assert.IsNotNull(collection.SelectedItem);
            Assert.AreSame(items[0], collection.SelectedItem);

            items = new SelectableCollectionItem<int>[]
            {
                new SelectableCollectionItem<int>(6, false),
                new SelectableCollectionItem<int>(12, true)
            };

            collection = new SelectableItemCollection<SelectableCollectionItem<int>>(items.ToList());
            expected = 2;
            actual = collection.Count;
            Assert.AreEqual(expected, actual);
            expected = 1;
            actual = collection.SelectedItems.Count;
            Assert.AreEqual(expected, actual);
            expected = 1;
            actual = collection.SelectedIndex;
            Assert.AreEqual(expected, actual);
            Assert.IsNotNull(collection.SelectedItem);
            Assert.AreSame(items[1], collection.SelectedItem);
        }
    }
}
