using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BlubLib.Buffers;

namespace BlubLib.IO
{
    public static class BinaryReaderExtensions
    {
        public static byte[] ReadToEnd(this BinaryReader @this)
        {
            return @this.BaseStream.ReadToEnd();
        }

        public static Task<byte[]> ReadToEndAsync(this BinaryReader @this)
        {
            return @this.BaseStream.ReadToEndAsync();
        }

        public static string[] ReadStrings(this BinaryReader @this, int count)
        {
            var array = new string[count];
            for (var i = 0; i < count; i++)
                array[i] = @this.ReadString();
            return array;
        }

        public static IPEndPoint ReadIPEndPoint(this BinaryReader @this)
        {
            var ip = new IPAddress(@this.ReadBytes(4));
            return new IPEndPoint(ip, @this.ReadUInt16());
        }

        public static string ReadCString(this BinaryReader @this)
        {
            var sb = new StringBuilder();
            char c;
            while ((c = @this.ReadChar()) != 0)
                sb.Append(c);

            return sb.ToString();
        }

        public static string ReadCString(this BinaryReader @this, int length)
        {
            if (length < 1)
                throw new ArgumentOutOfRangeException(nameof(length));

            return Encoding.UTF8.GetString(@this.ReadBytes(length)).TrimEnd('\0');
        }

        public static bool IsEOF(this BinaryReader @this)
        {
            return @this.BaseStream.IsEOF();
        }
    }

    public static class BinaryWriterExtensions
    {
        #region Write Arrays
        public static void Write(this BinaryWriter @this, IEnumerable<byte> values)
        {
            foreach (var value in values)
                @this.Write(value);
        }

        public static void Write(this BinaryWriter @this, IEnumerable<bool> values)
        {
            foreach (var value in values)
                @this.Write(value);
        }

        public static void Write(this BinaryWriter @this, IEnumerable<char> values)
        {
            foreach (var value in values)
                @this.Write(value);
        }

        public static void Write(this BinaryWriter @this, IEnumerable<sbyte> values)
        {
            foreach (var value in values)
                @this.Write(value);
        }

        public static void Write(this BinaryWriter @this, IEnumerable<short> values)
        {
            foreach (var value in values)
                @this.Write(value);
        }

        public static void Write(this BinaryWriter @this, IEnumerable<ushort> values)
        {
            foreach (var value in values)
                @this.Write(value);
        }

        public static void Write(this BinaryWriter @this, IEnumerable<int> values)
        {
            foreach (var value in values)
                @this.Write(value);
        }

        public static void Write(this BinaryWriter @this, IEnumerable<uint> values)
        {
            foreach (var value in values)
                @this.Write(value);
        }

        public static void Write(this BinaryWriter @this, IEnumerable<long> values)
        {
            foreach (var value in values)
                @this.Write(value);
        }

        public static void Write(this BinaryWriter @this, IEnumerable<ulong> values)
        {
            foreach (var value in values)
                @this.Write(value);
        }

        public static void Write(this BinaryWriter @this, IEnumerable<float> values)
        {
            foreach (var value in values)
                @this.Write(value);
        }

        public static void Write(this BinaryWriter @this, IEnumerable<double> values)
        {
            foreach (var value in values)
                @this.Write(value);
        }

        public static void Write(this BinaryWriter @this, IEnumerable<decimal> values)
        {
            foreach (var value in values)
                @this.Write(value);
        }

        public static void Write(this BinaryWriter @this, IEnumerable<string> values)
        {
            foreach (var value in values)
                @this.Write(value);
        }
        #endregion

        public static void Write(this BinaryWriter @this, IPEndPoint value)
        {
            @this.Write(value.Address.GetAddressBytes());
            @this.Write((ushort)value.Port);
        }

        public static void Write(this BinaryWriter @this, ArraySegment<byte> segment)
        {
            @this.Write(segment.Array, segment.Offset, segment.Count);
        }

        public static void WriteCString(this BinaryWriter @this, string value)
        {
            var buffer = Encoding.UTF8.GetBytes(value + "\0");
            @this.Write(buffer);
        }

        public static void WriteCString(this BinaryWriter @this, string value, int maxLength)
        {
            value = value ?? "";
            var buffer = Encoding.UTF8.GetBytes(value + "\0");
            if (buffer.Length > maxLength)
                throw new ArgumentOutOfRangeException($"{nameof(value)} is longer than {nameof(maxLength)}", nameof(value));

            @this.Write(buffer);
            @this.Fill(maxLength - buffer.Length);
        }

        public static void Fill(this BinaryWriter @this, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (count == 0)
                return;

            if (count < 256)
            {
                for (var i = 0; i < count; ++i)
                    @this.Write((byte)0);
            }
            else
            {
                @this.Write(new byte[count]);
            }
        }

        public static bool IsEOF(this BinaryWriter @this)
        {
            return @this.BaseStream.IsEOF();
        }

        public static byte[] ToArray(this BinaryWriter @this)
        {
            switch (@this.BaseStream)
            {
                case MemoryStream memoryStream:
                    return memoryStream.ToArray();

                case BufferStream bufferStream:
                    return bufferStream.ToArray();
            }

            throw new InvalidOperationException("BaseStream must be a MemoryStream or BufferStream");
        }
    }

    public static class StreamExtensions
    {
        public static BinaryReader ToBinaryReader(this Stream @this, Encoding encoding, bool leaveOpen)
        {
            return new BinaryReader(@this, encoding, leaveOpen);
        }

        public static BinaryReader ToBinaryReader(this Stream @this, bool leaveOpen)
        {
            return new BinaryReader(@this, Encoding.UTF8, leaveOpen);
        }

        public static BinaryWriter ToBinaryWriter(this Stream @this, Encoding encoding, bool leaveOpen)
        {
            return new BinaryWriter(@this, encoding, leaveOpen);
        }

        public static BinaryWriter ToBinaryWriter(this Stream @this, bool leaveOpen)
        {
            return new BinaryWriter(@this, Encoding.UTF8, leaveOpen);
        }

        public static byte[] ReadToEnd(this Stream @this)
        {
            using (var ms = new MemoryStream())
            {
                @this.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static async Task<byte[]> ReadToEndAsync(this Stream @this)
        {
            using (var ms = new MemoryStream())
            {
                await @this.CopyToAsync(ms).ConfigureAwait(false);
                return ms.ToArray();
            }
        }

        public static bool IsEOF(this Stream @this)
        {
            return @this.Position == @this.Length;
        }

        public static byte[] CompressGZip(this Stream @this)
        {
            using (var ms = new MemoryStream())
            using (var stream = new GZipStream(@this, CompressionMode.Compress, true))
            {
                stream.CopyTo(ms);
                stream.Flush();
                return ms.ToArray();
            }
        }

        public static void CompressGZip(this Stream @this, Stream output)
        {
            using (var stream = new GZipStream(@this, CompressionMode.Compress, true))
            {
                stream.CopyTo(output);
                stream.Flush();
            }
        }

        public static byte[] CompressDeflate(this Stream @this)
        {
            using (var ms = new MemoryStream())
            using (var stream = new DeflateStream(@this, CompressionMode.Compress, true))
            {
                stream.CopyTo(ms);
                stream.Flush();
                return ms.ToArray();
            }
        }

        public static void CompressDeflate(this Stream @this, Stream output)
        {
            using (var stream = new DeflateStream(@this, CompressionMode.Compress, true))
            {
                stream.CopyTo(output);
                stream.Flush();
            }
        }

        public static byte[] DecompressGZip(this Stream @this)
        {
            using (var stream = new GZipStream(@this, CompressionMode.Decompress, true))
                return stream.ReadToEnd();
        }

        public static void DecompressGZip(this Stream @this, Stream output)
        {
            using (var stream = new GZipStream(@this, CompressionMode.Decompress, true))
                stream.CopyTo(output);
        }

        public static byte[] DecompressDeflate(this Stream @this)
        {
            using (var stream = new DeflateStream(@this, CompressionMode.Decompress, true))
                return stream.ReadToEnd();
        }

        public static void DecompressDeflate(this Stream @this, Stream output)
        {
            using (var stream = new DeflateStream(@this, CompressionMode.Decompress, true))
                stream.CopyTo(output);
        }
    }
}
