using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace UnitTestHelpers
{
    /// <summary>
    /// Helper class which can be used to load or save test data.
    /// </summary>
    /// <typeparam name="TTestData">Type of test data object.</typeparam>
    public class SerializedTestDataLoader<TTestData>
        where TTestData : TestData
    {
        /// <summary>
        /// Creates default serializer for <typeparamref name="TTestData"/>.
        /// </summary>
        /// <returns>Serializer which can be used to serialize and deserialize <typeparamref name="TTestData"/> objects.</returns>
        /// <exception cref="System.InvalidOperationException">Unable to create serializer object.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static XmlSerializer CreateDefaultSerializer()
        {
            XmlSerializer xmlSerializer;

            try
            {
                xmlSerializer = new XmlSerializer(typeof(TTestData));
            }
            catch (Exception exc)
            {
                throw new InvalidOperationException(String.Format("Unable to create serializer object for type {0}: {1}", typeof(TTestData).FullName, exc.Message), exc);
            }

            return xmlSerializer;
        }

        /// <summary>
        /// Create default <see cref="System.Xml.XmlReaderSettings"/> to be used for <typeparamref name="TTestData"/> deserialization.
        /// </summary>
        /// <returns><see cref="System.Xml.XmlReaderSettings"/> with <see cref="System.Xml.XmlReaderSettings.CloseInput"/> set to false.</returns>
        public static XmlReaderSettings CreateDefaultReaderSettings()
        {
            return SerializedTestDataLoader<TTestData>.CreateDefaultReaderSettings(false);
        }

        /// <summary>
        /// Create default <see cref="System.Xml.XmlReaderSettings"/> to be used for <typeparamref name="TTestData"/> deserialization, specifying the <see cref="System.Xml.XmlReaderSettings.CloseInput"/> setting.
        /// </summary>
        /// <param name="closeInput">Set to true to close the underlying stream or <see cref="System.IO.TextReader"/> when the reader is closed; otherwise false.</param>
        /// <returns><see cref="System.Xml.XmlReaderSettings"/> with <see cref="System.Xml.XmlReaderSettings.CloseInput"/> set to the value of the <paramref name="closeInput"/> parameter.</returns>
        public static XmlReaderSettings CreateDefaultReaderSettings(bool closeInput)
        {
            return new XmlReaderSettings { CloseInput = closeInput };
        }

        /// <summary>
        /// Create default <see cref="System.Xml.XmlWriterSettings"/> to be used for <typeparamref name="TTestData"/> serialization.
        /// </summary>
        /// <returns><see cref="System.Xml.XmlWriterSettings"/> with <see cref="System.Xml.XmlWriterSettings.CloseInput"/> set to false.</returns>
        /// <remarks>This also sets <see cref="System.Xml.XmlWriterSettings.Encoding"/> to <see cref="System.Text.Encoding.UTF8"/>,
        /// <see cref="System.Xml.XmlWriterSettings.Indent"/> to true, and <see cref="System.Xml.XmlWriterSettings.WriteEndDocumentOnClose"/> to true.</remarks>
        public static XmlWriterSettings CreateDefaultWriterSettings()
        {
            return SerializedTestDataLoader<TTestData>.CreateDefaultWriterSettings(false);
        }

        /// <summary>
        /// Create default <see cref="System.Xml.XmlWriterSettings"/> to be used for <typeparamref name="TTestData"/> serialization, specifying the <see cref="System.Xml.XmlWriterSettings.CloseInput"/> setting.
        /// </summary>
        /// <param name="closeOutput">Set to true to close the underlying stream or <see cref="System.IO.TextWriter"/> when the writer is closed; otherwise false.</param>
        /// <returns><see cref="System.Xml.XmlWriterSettings"/> with <see cref="System.Xml.XmlWriterSettings.CloseInput"/> set to the value of the <paramref name="closeOutput"/> parameter.</returns>
        /// <remarks>This also sets <see cref="System.Xml.XmlWriterSettings.Encoding"/> to <see cref="System.Text.Encoding.UTF8"/>,
        /// <see cref="System.Xml.XmlWriterSettings.Indent"/> to true, and <see cref="System.Xml.XmlWriterSettings.WriteEndDocumentOnClose"/> to true.</remarks>
        public static XmlWriterSettings CreateDefaultWriterSettings(bool closeOutput)
        {
            return new XmlWriterSettings
            {
                CloseOutput = closeOutput,
                Encoding = System.Text.Encoding.UTF8,
                Indent = true,
                WriteEndDocumentOnClose = true
            };
        }

        /// <summary>
        /// Load serialized <typeparamref name="TTestData"/> object from a file containing XML representation of test data.
        /// </summary>
        /// <param name="fileName">Name, path or relative path to xml-serialized test data.</param>
        /// <returns>Deserialized <typeparamref name="TTestData"/> object.</returns>
        /// <remarks>If just the file name or relative path is provided, it will be treated as a path which is relative to the unit test's output directory.</remarks>
        /// <exception cref="System.ArgumentNullException"><paramref name="fileName"/> is null.</exception>
        /// <exception cref="System.ArgumentException"><paramref name="fileName"/> is a zero-length string, contains only white space,
        /// or contains one or more invalid characters as defined by <see cref="System.IO.Path.InvalidPathChars"/>.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.IO.FileNotFoundException"><paramref name="fileName"/> is cannot be found.</exception>
        /// <exception cref="System.IO.PathTooLongException"> The specified path, file name, or both exceed the system-defined maximum length.
        /// For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
        /// <exception cref="System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="System.UnauthorizedAccessException"><paramref name="fileName"/> specified a directory
        /// or thhe caller does not have the required permission.</exception>
        /// <exception cref="System.NotSupportedException"><paramref name="fileName"/> is in an invalid format.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during deserialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static TTestData Load(string fileName)
        {
            return SerializedTestDataLoader<TTestData>.Load(fileName, null as XmlSerializer);
        }

        /// <summary>
        /// Load serialized <typeparamref name="TTestData"/> object from a file containing XML representation of test data using a custom <see cref="System.Xml.Serialization.XmlSerializer"/> object.
        /// </summary>
        /// <param name="fileName">Name, path or relative path to xml-serialized test data.</param>
        /// <param name="xmlSerializer">Custom <see cref="System.Xml.Serialization.XmlSerializer"/> object to use or null.</param>
        /// <returns>Deserialized <typeparamref name="TTestData"/> object.</returns>
        /// <remarks>If just the file name or relative path is provided, it will be treated as a path which is relative to the unit test's output directory.
        /// <para>If <paramref name="xmlSerializer"/> is null, then <see cref=""/> is used to obtain the default <see cref="System.Xml.Serialization.XmlSerializer"/>.</para></remarks>
        /// <exception cref="System.ArgumentNullException"><paramref name="fileName"/> is null.</exception>
        /// <exception cref="System.ArgumentException"><paramref name="fileName"/> is a zero-length string, contains only white space,
        /// or contains one or more invalid characters as defined by <see cref="System.IO.Path.InvalidPathChars"/>.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.IO.FileNotFoundException"><paramref name="fileName"/> is cannot be found.</exception>
        /// <exception cref="System.IO.PathTooLongException"> The specified path, file name, or both exceed the system-defined maximum length.
        /// For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
        /// <exception cref="System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="System.UnauthorizedAccessException"><paramref name="fileName"/> specified a directory
        /// or thhe caller does not have the required permission.</exception>
        /// <exception cref="System.NotSupportedException"><paramref name="fileName"/> is in an invalid format.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during deserialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static TTestData Load(string fileName, XmlSerializer xmlSerializer)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");

            if (String.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be empty.", "fileName");

            bool isPathRooted;
            string path;
            try
            {
                isPathRooted = Path.IsPathRooted(fileName);
                path = (isPathRooted) ? fileName : Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception exc)
            {
                throw new ArgumentException("Unable to verify file name", "fileName", exc);
            }

            bool exists;
            try
            {
                exists = File.Exists(path);
            }
            catch (Exception exc)
            {
                throw new ArgumentException(String.Format("Unable to verify source path {0}", path), "fileName", exc);
            }

            if (!exists)
            {
                if (isPathRooted)
                    throw new FileNotFoundException("Unable to find source file", path);

                throw new FileNotFoundException("Unable to find source file. Perhaps the source file's build action is not set to 'Conent' or it is not configured to be copied to the output directory.", path);
            }

            TTestData result;

            using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                result = SerializedTestDataLoader<TTestData>.Load(stream, xmlSerializer);
            }

            return result;
        }

        /// <summary>
        /// Load serialized <typeparamref name="TTestData"/> object from a <see cref="System.IO.Stream"/> containing XML representation of test data.
        /// </summary>
        /// <param name="stream"><see cref="System.IO.Stream"/> containing xml-serialized test data.</param>
        /// <returns>Deserialized <typeparamref name="TTestData"/> object.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during deserialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static TTestData Load(Stream stream)
        {
            return SerializedTestDataLoader<TTestData>.Load(stream, null as XmlSerializer);
        }

        /// <summary>
        /// Load serialized <typeparamref name="TTestData"/> object from a <see cref="System.IO.Stream"/> containing XML representation of test data using custom <see cref="System.Xml.Serialization.XmlSerializer"/>.
        /// </summary>
        /// <param name="stream"><see cref="System.IO.Stream"/> containing xml-serialized test data.</param>
        /// <param name="xmlSerializer">Custom <see cref="System.Xml.Serialization.XmlSerializer"/> to use or null.</param>
        /// <returns>Deserialized <typeparamref name="TTestData"/> object.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during deserialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static TTestData Load(Stream stream, XmlSerializer xmlSerializer)
        {
            return SerializedTestDataLoader<TTestData>.Load(stream, xmlSerializer, null as XmlReaderSettings);
        }

        /// <summary>
        /// Load serialized <typeparamref name="TTestData"/> object from a <see cref="System.IO.Stream"/> containing XML representation of test data, using custom <see cref="System.Xml.XmlReaderSettings"/>.
        /// </summary>
        /// <param name="stream"><see cref="System.IO.Stream"/> containing xml-serialized test data.</param>
        /// <param name="settings">Custom <see cref="System.Xml.XmlReaderSettings"/> or null.</param>
        /// <returns>Deserialized <typeparamref name="TTestData"/> object.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during deserialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static TTestData Load(Stream stream, XmlReaderSettings settings)
        {
            return SerializedTestDataLoader<TTestData>.Load(stream, null as XmlSerializer, settings);
        }

        /// <summary>
        /// Load serialized <typeparamref name="TTestData"/> object from a <see cref="System.IO.Stream"/> containing XML representation of test data, using custom <see cref="System.Xml.Serialization.XmlSerializer"/> and <see cref="System.Xml.XmlReaderSettings"/>.
        /// </summary>
        /// <param name="stream"><see cref="System.IO.Stream"/> containing xml-serialized test data.</param>
        /// <param name="xmlSerializer">Custom <see cref="System.Xml.Serialization.XmlSerializer"/> to use or null.</param>
        /// <param name="settings">Custom <see cref="System.Xml.XmlReaderSettings"/> or null.</param>
        /// <returns>Deserialized <typeparamref name="TTestData"/> object.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during deserialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static TTestData Load(Stream stream, XmlSerializer xmlSerializer, XmlReaderSettings settings)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            TTestData result;

            using (XmlReader reader = XmlReader.Create(stream, (settings == null) ? SerializedTestDataLoader<TTestData>.CreateDefaultReaderSettings() : settings))
            {
                result = SerializedTestDataLoader<TTestData>.Load(reader, xmlSerializer);
            }

            return result;
        }

        /// <summary>
        /// Load serialized <typeparamref name="TTestData"/> object from an <see cref="System.Xml.XmlReader"/> containing test data.
        /// </summary>
        /// <param name="reader"><see cref="System.Xml.XmlReader"/> containing serialized test data.</param>
        /// <returns>Deserialized <typeparamref name="TTestData"/> object.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="reader"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during deserialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static TTestData Load(XmlReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            return SerializedTestDataLoader<TTestData>.Load(reader, null as XmlSerializer);
        }

        /// <summary>
        /// Load serialized <typeparamref name="TTestData"/> object from an <see cref="System.Xml.XmlReader"/> containing test data, using a custom <see cref="System.Xml.Serialization.XmlSerializer"/>.
        /// </summary>
        /// <param name="reader"><see cref="System.Xml.XmlReader"/> containing serialized test data.</param>
        /// <param name="xmlSerializer">Custom <see cref="System.Xml.Serialization.XmlSerializer"/> to use or null.</param>
        /// <returns>Deserialized <typeparamref name="TTestData"/> object.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="reader"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during deserialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static TTestData Load(XmlReader reader, XmlSerializer xmlSerializer)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            return (TTestData)(((xmlSerializer == null) ? SerializedTestDataLoader<TTestData>.CreateDefaultSerializer() : xmlSerializer)
                .Deserialize(reader));
        }

        /// <summary>
        /// Load serialized <typeparamref name="TTestData"/> object from a <see cref="System.Xml.XmlReader"/> containing test data, specifing the encoding which is used.
        /// </summary>
        /// <param name="reader"><see cref="System.Xml.XmlReader"/> containing serialized test data.</param>
        /// <param name="xmlSerializer">Custom <see cref="System.Xml.Serialization.XmlSerializer"/> to use or null.</param>
        /// <param name="encodingStyle">The encoding used or null for default.</param>
        /// <returns>Deserialized <typeparamref name="TTestData"/> object.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="reader"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during deserialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static TTestData Load(XmlReader reader, XmlSerializer xmlSerializer, string encodingStyle)
        {
            if (String.IsNullOrEmpty(encodingStyle))
                SerializedTestDataLoader<TTestData>.Load(reader, xmlSerializer);

            if (reader == null)
                throw new ArgumentNullException("reader");

            return (TTestData)(((xmlSerializer == null) ? SerializedTestDataLoader<TTestData>.CreateDefaultSerializer() : xmlSerializer)
                .Deserialize(reader, encodingStyle));
        }

        /// <summary>
        /// Load serialized <typeparamref name="TTestData"/> object from a <see cref="System.Xml.XmlReader"/> containing test data, using custom parameters.
        /// </summary>
        /// <param name="reader"><see cref="System.Xml.XmlReader"/> containing serialized test data.</param>
        /// <param name="xmlSerializer">Custom <see cref="System.Xml.Serialization.XmlSerializer"/> to use or null.</param>
        /// <param name="events"> An instance of the <see cref="System.Xml.Serialization.XmlDeserializationEvents"/> structure to handle events during deserialization.</param>
        /// <returns>Deserialized <typeparamref name="TTestData"/> object.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="reader"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during deserialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static TTestData Load(XmlReader reader, XmlSerializer xmlSerializer, XmlDeserializationEvents events)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            return (TTestData)(((xmlSerializer == null) ? SerializedTestDataLoader<TTestData>.CreateDefaultSerializer() : xmlSerializer)
                .Deserialize(reader, events));
        }

        /// <summary>
        /// Load serialized <typeparamref name="TTestData"/> object from a <see cref="System.Xml.XmlReader"/> containing test data, using custom parameters.
        /// </summary>
        /// <param name="reader"><see cref="System.Xml.XmlReader"/> containing serialized test data.</param>
        /// <param name="xmlSerializer">Custom <see cref="System.Xml.Serialization.XmlSerializer"/> to use or null.</param>
        /// <param name="encodingStyle">The encoding used or null for default.</param>
        /// <param name="events"> An instance of the <see cref="System.Xml.Serialization.XmlDeserializationEvents"/> structure to handle events during deserialization.</param>
        /// <returns>Deserialized <typeparamref name="TTestData"/> object.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="reader"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during deserialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static TTestData Load(XmlReader reader, XmlSerializer xmlSerializer, string encodingStyle, XmlDeserializationEvents events)
        {
            if (String.IsNullOrEmpty(encodingStyle))
                return SerializedTestDataLoader<TTestData>.Load(reader, xmlSerializer, events);

            if (reader == null)
                throw new ArgumentNullException("reader");

            return (TTestData)(((xmlSerializer == null) ? SerializedTestDataLoader<TTestData>.CreateDefaultSerializer() : xmlSerializer)
                .Deserialize(reader, encodingStyle, events));
        }

        public static void Save(TTestData obj, string fileName)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, fileName, null as XmlSerializer);
        }

        public static void Save(TTestData obj, string fileName, bool overwrite)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, fileName, overwrite, null as XmlSerializer);
        }

        public static void Save(TTestData obj, string fileName, XmlSerializer xmlSerializer)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, fileName, false, xmlSerializer);
        }

        public static void Save(TTestData obj, string fileName, bool overwrite, XmlSerializer xmlSerializer)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");

            if (String.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be empty.", "fileName");

            bool isPathRooted;
            string path;
            try
            {
                isPathRooted = Path.IsPathRooted(fileName);
                path = (isPathRooted) ? fileName : Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception exc)
            {
                throw new ArgumentException("Unable to verify file name", "fileName", exc);
            }

            using (FileStream stream = File.Open(path, (overwrite) ? FileMode.Create : FileMode.CreateNew, FileAccess.Write))
            {
                SerializedTestDataLoader<TTestData>.Save(obj, stream, xmlSerializer);
            }
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.IO.Stream"/>.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="stream">The <see cref="System.IO.Stream"/> to which the XML document is written.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, Stream stream)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, stream, null as XmlSerializer);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.IO.Stream"/>, using custom <see cref="System.Xml.Serialization.XmlSerializer"/>.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="stream">The <see cref="System.IO.Stream"/> to which the XML document is written.</param>
        /// <param name="xmlSerializer">Custom <see cref="System.Xml.Serialization.XmlSerializer"/> to use or null.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, Stream stream, XmlSerializer xmlSerializer)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, stream, xmlSerializer, null as XmlWriterSettings);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.IO.Stream"/>, using custom <see cref="System.Xml.XmlWriterSettings"/>.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="stream">The <see cref="System.IO.Stream"/> to which the XML document is written.</param>
        /// <param name="settings"><see cref="System.Xml.XmlWriterSettings"/> object containing settings used for generating the XML document. This can be null to use default settings.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, Stream stream, XmlWriterSettings settings)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, stream, null as XmlSerializer, settings);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.IO.Stream"/>, using custom parameters.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="stream">The <see cref="System.IO.Stream"/> to which the XML document is written.</param>
        /// <param name="xmlSerializer">Custom <see cref="System.Xml.Serialization.XmlSerializer"/> to use or null.</param>
        /// <param name="settings"><see cref="System.Xml.XmlWriterSettings"/> object containing settings used for generating the XML document. This can be null to use default settings.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, Stream stream, XmlSerializer xmlSerializer, XmlWriterSettings settings)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, stream, xmlSerializer, settings, null as XmlSerializerNamespaces);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.IO.Stream"/>, using custom <see cref="System.Xml.Serialization.XmlSerializerNamespaces"/>.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="stream">The <see cref="System.IO.Stream"/> to which the XML document is written.</param>
        /// <param name="namespaces">An instance of the <see cref="System.Xml.Serialization.XmlSerializerNamespaces"/> that contains namespaces and prefixes to use.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, Stream stream, XmlSerializerNamespaces namespaces)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, stream, null as XmlSerializer, namespaces);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.IO.Stream"/>, using custom parameters.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="stream">The <see cref="System.IO.Stream"/> to which the XML document is written.</param>
        /// <param name="xmlSerializer">Custom <see cref="System.Xml.Serialization.XmlSerializer"/> to use or null.</param>
        /// <param name="namespaces">An instance of the <see cref="System.Xml.Serialization.XmlSerializerNamespaces"/> that contains namespaces and prefixes to use.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, Stream stream, XmlSerializer xmlSerializer, XmlSerializerNamespaces namespaces)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, stream, xmlSerializer, null as XmlWriterSettings, namespaces);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.IO.Stream"/>, using custom parameters.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="stream">The <see cref="System.IO.Stream"/> to which the XML document is written.</param>\
        /// <param name="settings"><see cref="System.Xml.XmlWriterSettings"/> object containing settings used for generating the XML document. This can be null to use default settings.</param>
        /// <param name="namespaces">An instance of the <see cref="System.Xml.Serialization.XmlSerializerNamespaces"/> that contains namespaces and prefixes to use.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, Stream stream, XmlWriterSettings settings, XmlSerializerNamespaces namespaces)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, stream, null as XmlSerializer, settings, namespaces);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.IO.Stream"/>, using custom parameters.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="stream">The <see cref="System.IO.Stream"/> to which the XML document is written.</param>
        /// <param name="xmlSerializer">Custom <see cref="System.Xml.Serialization.XmlSerializer"/> to use or null.</param>
        /// <param name="settings"><see cref="System.Xml.XmlWriterSettings"/> object containing settings used for generating the XML document. This can be null to use default settings.</param>
        /// <param name="namespaces">An instance of the <see cref="System.Xml.Serialization.XmlSerializerNamespaces"/> that contains namespaces and prefixes to use.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, Stream stream, XmlSerializer xmlSerializer, XmlWriterSettings settings, XmlSerializerNamespaces namespaces)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, stream, xmlSerializer, settings, namespaces, null as string);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.IO.Stream"/>, using custom parameters.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="stream">The <see cref="System.IO.Stream"/> to which the XML document is written.</param>
        /// <param name="namespaces">An instance of the <see cref="System.Xml.Serialization.XmlSerializerNamespaces"/> that contains namespaces and prefixes to use.</param>
        /// <param name="encodingStyle">The encoding to be used in the resulting XML document.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, Stream stream, XmlSerializerNamespaces namespaces, string encodingStyle)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, stream, null as XmlSerializer, namespaces, encodingStyle);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.IO.Stream"/>, using custom parameters.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="stream">The <see cref="System.IO.Stream"/> to which the XML document is written.</param>
        /// <param name="xmlSerializer">Custom <see cref="System.Xml.Serialization.XmlSerializer"/> to use or null.</param>
        /// <param name="namespaces">An instance of the <see cref="System.Xml.Serialization.XmlSerializerNamespaces"/> that contains namespaces and prefixes to use.</param>
        /// <param name="encodingStyle">The encoding to be used in the resulting XML document.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, Stream stream, XmlSerializer xmlSerializer, XmlSerializerNamespaces namespaces, string encodingStyle)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, stream, xmlSerializer, null as XmlWriterSettings, namespaces, encodingStyle);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.IO.Stream"/>, using custom parameters.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="stream">The <see cref="System.IO.Stream"/> to which the XML document is written.</param>
        /// <param name="settings"><see cref="System.Xml.XmlWriterSettings"/> object containing settings used for generating the XML document. This can be null to use default settings.</param>
        /// <param name="namespaces">An instance of the <see cref="System.Xml.Serialization.XmlSerializerNamespaces"/> that contains namespaces and prefixes to use.</param>
        /// <param name="encodingStyle">The encoding to be used in the resulting XML document.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, Stream stream, XmlWriterSettings settings, XmlSerializerNamespaces namespaces, string encodingStyle)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, stream, null as XmlSerializer, settings, namespaces, encodingStyle);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.IO.Stream"/>, using custom parameters.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="stream">The <see cref="System.IO.Stream"/> to which the XML document is written.</param>
        /// <param name="xmlSerializer">Custom <see cref="System.Xml.Serialization.XmlSerializer"/> to use or null.</param>
        /// <param name="settings"><see cref="System.Xml.XmlWriterSettings"/> object containing settings used for generating the XML document. This can be null to use default settings.</param>
        /// <param name="namespaces">An instance of the <see cref="System.Xml.Serialization.XmlSerializerNamespaces"/> that contains namespaces and prefixes to use.</param>
        /// <param name="encodingStyle">The encoding to be used in the resulting XML document.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, Stream stream, XmlSerializer xmlSerializer, XmlWriterSettings settings, XmlSerializerNamespaces namespaces, string encodingStyle)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, stream, xmlSerializer, settings, namespaces, encodingStyle, null as string);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.IO.Stream"/>, using custom parameters.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="stream">The <see cref="System.IO.Stream"/> to which the XML document is written.</param>
        /// <param name="namespaces">An instance of the <see cref="System.Xml.Serialization.XmlSerializerNamespaces"/> that contains namespaces and prefixes to use.</param>
        /// <param name="encodingStyle">The encoding to be used in the resulting XML document.</param>
        /// <param name="id">The base used to generate id attributes (typically used for SOAP generating encoded messages).</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, Stream stream, XmlSerializerNamespaces namespaces, string encodingStyle, string id)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, stream, null as XmlSerializer, namespaces, encodingStyle, id);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.IO.Stream"/>, using custom parameters.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="stream">The <see cref="System.IO.Stream"/> to which the XML document is written.</param>
        /// <param name="xmlSerializer">Custom <see cref="System.Xml.Serialization.XmlSerializer"/> to use or null.</param>
        /// <param name="namespaces">An instance of the <see cref="System.Xml.Serialization.XmlSerializerNamespaces"/> that contains namespaces and prefixes to use.</param>
        /// <param name="encodingStyle">The encoding to be used in the resulting XML document.</param>
        /// <param name="id">The base used to generate id attributes (typically used for SOAP generating encoded messages).</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, Stream stream, XmlSerializer xmlSerializer, XmlSerializerNamespaces namespaces, string encodingStyle, string id)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, stream, xmlSerializer, null as XmlWriterSettings, namespaces, encodingStyle, id);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.IO.Stream"/>, using custom parameters.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="stream">The <see cref="System.IO.Stream"/> to which the XML document is written.</param>
        /// <param name="settings"><see cref="System.Xml.XmlWriterSettings"/> object containing settings used for generating the XML document. This can be null to use default settings.</param>
        /// <param name="namespaces">An instance of the <see cref="System.Xml.Serialization.XmlSerializerNamespaces"/> that contains namespaces and prefixes to use.</param>
        /// <param name="encodingStyle">The encoding to be used in the resulting XML document.</param>
        /// <param name="id">The base used to generate id attributes (typically used for SOAP generating encoded messages).</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, Stream stream, XmlWriterSettings settings, XmlSerializerNamespaces namespaces, string encodingStyle, string id)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, stream, null as XmlSerializer, settings, namespaces, encodingStyle, id);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.IO.Stream"/>, using custom parameters.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="stream">The <see cref="System.IO.Stream"/> to which the XML document is written.</param>
        /// <param name="xmlSerializer">Custom <see cref="System.Xml.Serialization.XmlSerializer"/> to use or null.</param>
        /// <param name="settings"><see cref="System.Xml.XmlWriterSettings"/> object containing settings used for generating the XML document. This can be null to use default settings.</param>
        /// <param name="namespaces">An instance of the <see cref="System.Xml.Serialization.XmlSerializerNamespaces"/> that contains namespaces and prefixes to use.</param>
        /// <param name="encodingStyle">The encoding to be used in the resulting XML document.</param>
        /// <param name="id">The base used to generate id attributes (typically used for SOAP generating encoded messages).</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, Stream stream, XmlSerializer xmlSerializer, XmlWriterSettings settings, XmlSerializerNamespaces namespaces, string encodingStyle, string id)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            using (XmlWriter writer = XmlWriter.Create(stream, (settings == null) ? SerializedTestDataLoader<TTestData>.CreateDefaultWriterSettings() : settings))
            {
                SerializedTestDataLoader<TTestData>.Save(obj, writer, xmlSerializer, namespaces, encodingStyle, id);
            }
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.Xml.XmlWriter"/>.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="writer">The <see cref="System.Xml.XmlWriter"/> used to write the XML document.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="writer"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, XmlWriter writer)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, writer, null as XmlSerializer);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.Xml.XmlWriter"/>, using a custom <see cref="System.Xml.Serialization.XmlSerializer"/>.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="writer">The <see cref="System.Xml.XmlWriter"/> used to write the XML document.</param>
        /// <param name="xmlSerializer">Custom <see cref="System.Xml.Serialization.XmlSerializer"/> to use or null.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="writer"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, XmlWriter writer, XmlSerializer xmlSerializer)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, writer, xmlSerializer, null as XmlSerializerNamespaces);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.Xml.XmlWriter"/>, using custom <see cref="System.Xml.Serialization.XmlSerializerNamespaces"/>.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="writer">The <see cref="System.Xml.XmlWriter"/> used to write the XML document.</param>
        /// <param name="namespaces">An instance of the <see cref="System.Xml.Serialization.XmlSerializerNamespaces"/> that contains namespaces and prefixes to use.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="writer"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, XmlWriter writer, XmlSerializerNamespaces namespaces)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, writer, null as XmlSerializer, namespaces);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.Xml.XmlWriter"/>, using custom parameters.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="writer">The <see cref="System.Xml.XmlWriter"/> used to write the XML document.</param>
        /// <param name="xmlSerializer">Custom <see cref="System.Xml.Serialization.XmlSerializer"/> to use or null.</param>
        /// <param name="namespaces">An instance of the <see cref="System.Xml.Serialization.XmlSerializerNamespaces"/> that contains namespaces and prefixes to use.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="writer"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, XmlWriter writer, XmlSerializer xmlSerializer, XmlSerializerNamespaces namespaces)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, writer, xmlSerializer, namespaces, null as string);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.Xml.XmlWriter"/>, using custom parameters.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="writer">The <see cref="System.Xml.XmlWriter"/> used to write the XML document.</param>
        /// <param name="namespaces">An instance of the <see cref="System.Xml.Serialization.XmlSerializerNamespaces"/> that contains namespaces and prefixes to use.</param>
        /// <param name="encodingStyle">The encoding to be used in the resulting XML document.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="writer"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, XmlWriter writer, XmlSerializerNamespaces namespaces, string encodingStyle)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, writer, null as XmlSerializer, namespaces, encodingStyle);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.Xml.XmlWriter"/>, using custom parameters.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="writer">The <see cref="System.Xml.XmlWriter"/> used to write the XML document.</param>
        /// <param name="xmlSerializer">Custom <see cref="System.Xml.Serialization.XmlSerializer"/> to use or null.</param>
        /// <param name="namespaces">An instance of the <see cref="System.Xml.Serialization.XmlSerializerNamespaces"/> that contains namespaces and prefixes to use.</param>
        /// <param name="encodingStyle">The encoding to be used in the resulting XML document.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="writer"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, XmlWriter writer, XmlSerializer xmlSerializer, XmlSerializerNamespaces namespaces, string encodingStyle)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, writer, xmlSerializer, namespaces, encodingStyle, null as string);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.Xml.XmlWriter"/>, using custom parameters.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="writer">The <see cref="System.Xml.XmlWriter"/> used to write the XML document.</param>
        /// <param name="namespaces">An instance of the <see cref="System.Xml.Serialization.XmlSerializerNamespaces"/> that contains namespaces and prefixes to use.</param>
        /// <param name="encodingStyle">The encoding to be used in the resulting XML document.</param>
        /// <param name="id">The base used to generate id attributes (typically used for SOAP generating encoded messages).</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="writer"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, XmlWriter writer, XmlSerializerNamespaces namespaces, string encodingStyle, string id)
        {
            SerializedTestDataLoader<TTestData>.Save(obj, writer, null as XmlSerializer, namespaces, encodingStyle, id);
        }

        /// <summary>
        /// Saves (serializes) a <typeparamref name="TTestData"/> object to an <see cref="System.Xml.XmlWriter"/>, using custom parameters.
        /// </summary>
        /// <param name="obj"><typeparamref name="TTestData"/> object to be serialized.</param>
        /// <param name="writer">The <see cref="System.Xml.XmlWriter"/> used to write the XML document.</param>
        /// <param name="xmlSerializer">Custom <see cref="System.Xml.Serialization.XmlSerializer"/> to use or null.</param>
        /// <param name="namespaces">An instance of the <see cref="System.Xml.Serialization.XmlSerializerNamespaces"/> that contains namespaces and prefixes to use.</param>
        /// <param name="encodingStyle">The encoding to be used in the resulting XML document.</param>
        /// <param name="id">The base used to generate id attributes (typically used for SOAP generating encoded messages).</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="obj"/> or <paramref name="writer"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">An error occurred during serialization.
        /// The original exception is available using the <see cref="System.Exception.InnerException"/> property.</exception>
        public static void Save(TTestData obj, XmlWriter writer, XmlSerializer xmlSerializer, XmlSerializerNamespaces namespaces, string encodingStyle, string id)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            if (writer == null)
                throw new ArgumentNullException("writer");

            if (xmlSerializer == null)
                xmlSerializer = SerializedTestDataLoader<TTestData>.CreateDefaultSerializer();

            if (String.IsNullOrEmpty(id))
            {
                if (String.IsNullOrEmpty(encodingStyle))
                {
                    if (namespaces == null)
                    {
                        xmlSerializer.Serialize(writer, obj);
                        return;
                    }
                    xmlSerializer.Serialize(writer, obj, namespaces);
                    return;
                }

                xmlSerializer.Serialize(writer, obj, namespaces, encodingStyle);
            }
            else
                xmlSerializer.Serialize(writer, obj, namespaces, encodingStyle, id);
        }
    }
}
