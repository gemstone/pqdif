//******************************************************************************************************
//  VectorElement.cs - Gbtc
//
//  Copyright © 2012, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the MIT License (MIT), the "License"; you may
//  not use this file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://www.opensource.org/licenses/MIT
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  05/02/2012 - Stephen C. Wills, Grid Protection Alliance
//       Generated original version of source code.
//  12/17/2012 - Starlynn Danyelle Gilliam
//       Modified Header.
//
//******************************************************************************************************

using System;
using System.Numerics;
using System.Text;

namespace Gemstone.PQDIF.Physical
{
    /// <summary>
    /// Represents an <see cref="Element"/> which is a collection of values
    /// in a PQDIF file. Vector elements are part of the physical structure
    /// of a PQDIF file. They exist within the body of a <see cref="Record"/>
    /// (contained by a <see cref="CollectionElement"/>).
    /// </summary>
    public class VectorElement : Element
    {
        #region [ Members ]

        // Fields
        private int m_size;
        private byte[]? m_values;

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets the number of values in the vector.
        /// </summary>
        public int Size
        {
            get
            {
                return m_size;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Size must be >= 0", nameof(value));

                if (m_size != value)
                {
                    m_size = value;
                    Reallocate();
                }
            }
        }

        /// <summary>
        /// Gets the type of the element.
        /// Returns <see cref="ElementType.Vector"/>.
        /// </summary>
        public override ElementType TypeOfElement
        {
            get
            {
                return ElementType.Vector;
            }
        }

        /// <summary>
        /// Gets or sets the physical type of the value or values contained
        /// by the element.
        /// </summary>
        /// <remarks>
        /// This determines the data type and size of the
        /// value or values. The value of this property is only relevant when
        /// <see cref="TypeOfElement"/> is either <see cref="ElementType.Scalar"/>
        /// or <see cref="ElementType.Vector"/>.
        /// </remarks>
        public override PhysicalType TypeOfValue
        {
            get
            {
                return base.TypeOfValue;
            }
            set
            {
                if (base.TypeOfValue != value)
                {
                    base.TypeOfValue = value;
                    Reallocate();
                }
            }
        }

        private byte[] Values
        {
            get
            {
                if (m_values is null)
                    throw new InvalidOperationException("Unable to retrieve values from uninitialized vector; set the size and physical type of the vector first");

                return m_values;
            }
            set
            {
                m_values = value;
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Gets the value at the given index as the physical type defined
        /// by <see cref="TypeOfValue"/> and returns it as a generic
        /// <see cref="object"/>.
        /// </summary>
        /// <param name="index">The index of the value to be retrieved.</param>
        /// <returns>The value that was retrieved from the vector.</returns>
        public object Get(int index)
        {
            return TypeOfValue switch
            {
                PhysicalType.Boolean1 => GetUInt1(index) != 0,
                PhysicalType.Boolean2 => GetInt2(index) != 0,
                PhysicalType.Boolean4 => GetInt4(index) != 0,
                PhysicalType.Char1 => Encoding.ASCII.GetString(Values)[index],
                PhysicalType.Char2 => Encoding.Unicode.GetString(Values)[index],
                PhysicalType.Integer1 => GetInt1(index),
                PhysicalType.Integer2 => GetInt2(index),
                PhysicalType.Integer4 => GetInt4(index),
                PhysicalType.UnsignedInteger1 => GetUInt1(index),
                PhysicalType.UnsignedInteger2 => GetUInt2(index),
                PhysicalType.UnsignedInteger4 => GetUInt4(index),
                PhysicalType.Real4 => GetReal4(index),
                PhysicalType.Real8 => GetReal8(index),
                PhysicalType.Complex8 => new Complex(GetReal4(index * 2), GetReal4(index * 2 + 1)),
                PhysicalType.Complex16 => new Complex(GetReal8(index * 2), GetReal8(index * 2 + 1)),
                PhysicalType.Timestamp => GetTimestamp(index),
                PhysicalType.Guid => GetGuid(index),
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        /// <summary>                
        /// Sets the value at the given index as the physical type defined by <see cref="TypeOfValue"/>.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <param name="value">The new value to be stored.</param>
        public void Set(int index, object value)
        {
            char c;
            byte[] bytes;
            Complex complex;

            switch (TypeOfValue)
            {
                case PhysicalType.Boolean1:
                    SetUInt1(index, Convert.ToBoolean(value) ? (byte)1 : (byte)0);
                    break;

                case PhysicalType.Boolean2:
                    SetInt2(index, Convert.ToBoolean(value) ? (short)1 : (short)0);
                    break;

                case PhysicalType.Boolean4:
                    SetInt4(index, Convert.ToBoolean(value) ? 1 : 0);
                    break;

                case PhysicalType.Char1:
                    c = Convert.ToChar(value);
                    bytes = Encoding.ASCII.GetBytes(c.ToString());
                    SetUInt1(index, bytes[0]);
                    break;

                case PhysicalType.Char2:
                    c = Convert.ToChar(value);
                    bytes = Encoding.Unicode.GetBytes(c.ToString());
                    SetInt2(index, BitConverter.ToInt16(bytes, 0));
                    break;

                case PhysicalType.Integer1:
                    SetInt1(index, Convert.ToSByte(value));
                    break;

                case PhysicalType.Integer2:
                    SetInt2(index, Convert.ToInt16(value));
                    break;

                case PhysicalType.Integer4:
                    SetInt4(index, Convert.ToInt32(value));
                    break;

                case PhysicalType.UnsignedInteger1:
                    SetUInt1(index, Convert.ToByte(value));
                    break;

                case PhysicalType.UnsignedInteger2:
                    SetUInt2(index, Convert.ToUInt16(value));
                    break;

                case PhysicalType.UnsignedInteger4:
                    SetUInt4(index, Convert.ToUInt32(value));
                    break;

                case PhysicalType.Real4:
                    SetReal4(index, Convert.ToSingle(value));
                    break;

                case PhysicalType.Real8:
                    SetReal8(index, Convert.ToDouble(value));
                    break;

                case PhysicalType.Complex8:
                    complex = (Complex)value;
                    SetReal4(index * 2, (float)complex.Real);
                    SetReal4(index * 2 + 1, (float)complex.Imaginary);
                    break;

                case PhysicalType.Complex16:
                    complex = (Complex)value;
                    SetReal8(index * 2, complex.Real);
                    SetReal8(index * 2 + 1, complex.Imaginary);
                    break;

                case PhysicalType.Timestamp:
                    SetTimestamp(index, Convert.ToDateTime(value));
                    break;

                case PhysicalType.Guid:
                    SetGuid(index, (Guid)value);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Gets a value in this vector as an 8-bit unsigned integer.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <returns>The value as an 8-bit unsigned integer.</returns>
        public byte GetUInt1(int index) => Values[index];

        /// <summary>
        /// Sets a value in this vector as an 8-bit unsigned integer.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <param name="value">The new value of an 8-bit unsigned integer.</param>
        public void SetUInt1(int index, byte value) => Values[index] = value;

        /// <summary>
        /// Gets a value in this vector as a 16-bit unsigned integer.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <returns>The value as a 16-bit unsigned integer.</returns>
        public ushort GetUInt2(int index)
        {
            int size = sizeof(ushort);
            int byteIndex = index * size;

            if (BitConverter.IsLittleEndian)
                return BitConverter.ToUInt16(Values, byteIndex);

            Span<byte> value = Values.AsSpan().Slice(byteIndex, size);
            Span<byte> copy = new byte[value.Length];
            value.CopyTo(copy);
            copy.Reverse();

        #if NETSTANDARD2_0
            return BitConverter.ToUInt16(copy.ToArray(), 0);
        #else
            return BitConverter.ToUInt16(copy);
        #endif
        }

        /// <summary>
        /// Sets a value in this vector as a 16-bit unsigned integer.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <param name="value">The new value of a 16-bit unsigned integer.</param>
        public void SetUInt2(int index, ushort value)
        {
            Span<byte> span = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
                span.Reverse();

            int byteIndex = index * span.Length;
            span.CopyTo(Values.AsSpan().Slice(byteIndex));
        }

        /// <summary>
        /// Gets a value in this vector as a 32-bit unsigned integer.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <returns>The value as a 32-bit unsigned integer.</returns>
        public uint GetUInt4(int index)
        {
            int size = sizeof(uint);
            int byteIndex = index * size;

            if (BitConverter.IsLittleEndian)
                return BitConverter.ToUInt32(Values, byteIndex);

            Span<byte> value = Values.AsSpan().Slice(byteIndex, size);
            Span<byte> copy = new byte[value.Length];
            value.CopyTo(copy);
            copy.Reverse();

        #if NETSTANDARD2_0
            return BitConverter.ToUInt32(copy.ToArray(), 0);
        #else
            return BitConverter.ToUInt32(copy);
        #endif
        }

        /// <summary>
        /// Sets a value in this vector as a 32-bit unsigned integer.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <param name="value">The new value of a 32-bit unsigned integer.</param>
        public void SetUInt4(int index, uint value)
        {
            Span<byte> span = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
                span.Reverse();

            int byteIndex = index * span.Length;
            span.CopyTo(Values.AsSpan().Slice(byteIndex));
        }

        /// <summary>
        /// Gets a value in this vector as an 8-bit signed integer.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <returns>The value as an 8-bit signed integer.</returns>
        public sbyte GetInt1(int index) => (sbyte)Values[index];

        /// <summary>
        /// Sets a value in this vector as an 8-bit signed integer.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <param name="value">The new value of an 8-bit signed integer.</param>
        public void SetInt1(int index, sbyte value) => Values[index] = (byte)value;

        /// <summary>
        /// Gets a value in this vector as a 16-bit signed integer.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <returns>The value as a 16-bit signed integer.</returns>
        public short GetInt2(int index)
        {
            int size = sizeof(short);
            int byteIndex = index * size;

            if (BitConverter.IsLittleEndian)
                return BitConverter.ToInt16(Values, byteIndex);

            Span<byte> value = Values.AsSpan().Slice(byteIndex, size);
            Span<byte> copy = new byte[value.Length];
            value.CopyTo(copy);
            copy.Reverse();

        #if NETSTANDARD2_0
            return BitConverter.ToInt16(copy.ToArray(), 0);
        #else
            return BitConverter.ToInt16(copy);
        #endif
        }

        /// <summary>
        /// Sets a value in this vector as a 16-bit signed integer.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <param name="value">The new value of a 16-bit signed integer.</param>
        public void SetInt2(int index, short value)
        {
            Span<byte> span = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
                span.Reverse();

            int byteIndex = index * span.Length;
            span.CopyTo(Values.AsSpan().Slice(byteIndex));
        }

        /// <summary>
        /// Gets a value in this vector as a 32-bit signed integer.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <returns>The value as a 32-bit signed integer.</returns>
        public int GetInt4(int index)
        {
            int size = sizeof(int);
            int byteIndex = index * size;

            if (BitConverter.IsLittleEndian)
                return BitConverter.ToInt32(Values, byteIndex);

            Span<byte> value = Values.AsSpan().Slice(byteIndex, size);
            Span<byte> copy = new byte[value.Length];
            value.CopyTo(copy);
            copy.Reverse();

        #if NETSTANDARD2_0
            return BitConverter.ToInt32(copy.ToArray(), 0);
        #else
            return BitConverter.ToInt32(copy);
        #endif
        }

        /// <summary>
        /// Sets a value in this vector as a 32-bit signed integer.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <param name="value">The new value of a 32-bit signed integer.</param>
        public void SetInt4(int index, int value)
        {
            Span<byte> span = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
                span.Reverse();

            int byteIndex = index * span.Length;
            span.CopyTo(Values.AsSpan().Slice(byteIndex));
        }

        /// <summary>
        /// Gets a value in this vector as a 32-bit floating point number.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <returns>The value as a 32-bit floating point number.</returns>
        public float GetReal4(int index)
        {
            int size = sizeof(float);
            int byteIndex = index * size;

            if (BitConverter.IsLittleEndian)
                return BitConverter.ToSingle(Values, byteIndex);

            Span<byte> value = Values.AsSpan().Slice(byteIndex, size);
            Span<byte> copy = new byte[value.Length];
            value.CopyTo(copy);
            copy.Reverse();

        #if NETSTANDARD2_0
            return BitConverter.ToSingle(copy.ToArray(), 0);
        #else
            return BitConverter.ToSingle(copy);
        #endif
        }

        /// <summary>
        /// Sets a value in this vector as a 32-bit floating point number.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <param name="value">The new value of a 32-bit floating point number.</param>
        public void SetReal4(int index, float value)
        {
            Span<byte> span = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
                span.Reverse();

            int byteIndex = index * span.Length;
            span.CopyTo(Values.AsSpan().Slice(byteIndex));
        }

        /// <summary>
        /// Gets a value in this vector as a 64-bit floating point number.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <returns>The value as a 64-bit floating point number.</returns>
        public double GetReal8(int index)
        {
            int size = sizeof(double);
            int byteIndex = index * size;

            if (BitConverter.IsLittleEndian)
                return BitConverter.ToDouble(Values, byteIndex);

            Span<byte> value = Values.AsSpan().Slice(byteIndex, size);
            Span<byte> copy = new byte[value.Length];
            value.CopyTo(copy);
            copy.Reverse();

        #if NETSTANDARD2_0
            return BitConverter.ToDouble(copy.ToArray(), 0);
        #else
            return BitConverter.ToDouble(copy);
        #endif
        }

        /// <summary>
        /// Sets a value in this vector as a 64-bit floating point number.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <param name="value">The new value of a 64-bit floating point number.</param>
        public void SetReal8(int index, double value)
        {
            Span<byte> span = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
                span.Reverse();

            int byteIndex = index * span.Length;
            span.CopyTo(Values.AsSpan().Slice(byteIndex));
        }

        /// <summary>
        /// Gets a value in this vector as a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <returns>The value as a <see cref="DateTime"/>.</returns>
        public DateTime GetTimestamp(int index)
        {
            int daySize = sizeof(uint);
            int secondSize = sizeof(double);
            int size = daySize + secondSize;

            int dayIndex = index * size;
            int secondIndex = dayIndex + daySize;

            Span<byte> daySpan = Values.AsSpan().Slice(dayIndex, daySize);
            Span<byte> secondSpan = Values.AsSpan().Slice(secondIndex, secondSize);

            if (!BitConverter.IsLittleEndian)
            {
                Span<byte> daySwap = new byte[daySize];
                daySpan.CopyTo(daySwap);
                daySwap.Reverse();
                daySpan = daySwap;

                Span<byte> secondSwap = new byte[secondSize];
                secondSpan.CopyTo(secondSwap);
                secondSwap.Reverse();
                secondSpan = secondSwap;
            }

        #if NETSTANDARD2_0
            uint days = BitConverter.ToUInt32(daySpan.ToArray(), 0);
            double seconds = BitConverter.ToDouble(secondSpan.ToArray(), 0);
        #else
            uint days = BitConverter.ToUInt32(daySpan);
            double seconds = BitConverter.ToDouble(secondSpan);
        #endif

            // Timestamps in a PQDIF file are represented by two separate numbers, one being the number of
            // days since January 1, 1900 and the other being the number of seconds since midnight. The
            // standard implementation also includes a constant for the number of days between January 1,
            // 1900 and January 1, 1970 to facilitate the conversion between PQDIF timestamps and UNIX
            // timestamps. However, the constant defined in the standard is 25569 days, whereas the actual
            // number of days between those two dates is 25567 days; a two day difference. That is why we
            // need to also subtract two days here when parsing PQDIF timestamps.
            DateTime epoch = new(1900, 1, 1);
            return epoch.AddDays(days - 2u).AddSeconds(seconds);
        }

        /// <summary>
        /// Sets a value in this vector as a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <param name="value">The new value of a <see cref="DateTime"/>.</param>
        public void SetTimestamp(int index, DateTime value)
        {
            DateTime epoch = new(1900, 1, 1);
            TimeSpan sinceEpoch = value - epoch;
            TimeSpan daysSinceEpoch = TimeSpan.FromDays(Math.Floor(sinceEpoch.TotalDays));
            TimeSpan secondsSinceMidnight = sinceEpoch - daysSinceEpoch;

            // Timestamps in a PQDIF file are represented by two separate numbers, one being the number of
            // days since January 1, 1900 and the other being the number of seconds since midnight. The
            // standard implementation also includes a constant for the number of days between January 1,
            // 1900 and January 1, 1970 to facilitate the conversion between PQDIF timestamps and UNIX
            // timestamps. However, the constant defined in the standard is 25569 days, whereas the actual
            // number of days between those two dates is 25567 days; a two day difference. That is why we
            // need to also add two days here when creating PQDIF timestamps.
            Span<byte> daySpan = BitConverter.GetBytes((uint)daysSinceEpoch.TotalDays + 2u);
            Span<byte> secondSpan = BitConverter.GetBytes(secondsSinceMidnight.TotalSeconds);

            if (!BitConverter.IsLittleEndian)
            {
                daySpan.Reverse();
                secondSpan.Reverse();
            }

            int daySize = daySpan.Length;
            int secondSize = secondSpan.Length;
            int size = daySize + secondSize;

            int dayIndex = index * size;
            int secondIndex = dayIndex + daySize;

            daySpan.CopyTo(Values.AsSpan().Slice(dayIndex));
            secondSpan.CopyTo(Values.AsSpan().Slice(secondIndex));
        }

        /// <summary>
        /// Gets the value in this vector as a globally unique identifier.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <returns>The value as a globally unique identifier.</returns>
        public Guid GetGuid(int index)
        {
            int size = 16;
            Span<byte> copy = new byte[size];
            int byteIndex = index * size;
            Values.AsSpan().Slice(byteIndex, size).CopyTo(copy);
        #if NETSTANDARD2_0
            return new Guid(copy.ToArray());
        #else
            return new Guid(copy);
        #endif
        }

        /// <summary>
        /// Sets the value in this vector as a globally unique identifier.
        /// </summary>
        /// <param name="index">The index of the value.</param>
        /// <param name="value">The new value as a globally unique identifier.</param>
        public void SetGuid(int index, Guid value)
        {
            byte[] bytes = value.ToByteArray();
            int byteIndex = index * bytes.Length;
            Buffer.BlockCopy(bytes, 0, Values, byteIndex, bytes.Length);
        }

        /// <summary>
        /// Gets the raw bytes of the values contained by this vector.
        /// </summary>
        /// <returns>The raw bytes of the values contained by this vector.</returns>
        public byte[] GetValues() =>
            Values;

        /// <summary>
        /// Sets the raw bytes of the values contained by this vector.
        /// </summary>
        /// <param name="values">The array that contains the raw bytes.</param>
        /// <param name="offset">The offset into the array at which the values start.</param>
        public void SetValues(byte[] values, int offset) =>
            Buffer.BlockCopy(values, offset, Values, 0, m_size * TypeOfValue.GetByteSize());

        /// <summary>
        /// Returns a string representation of this vector.
        /// </summary>
        /// <returns>A string representation of this vector.</returns>
        public override string ToString()
        {
            return $"Vector -- Type: {TypeOfValue}, Size: {m_size}, Tag: {TagOfElement}";
        }

        // Reallocates the byte array containing the vector data based on
        // the size of the vector and the physical type of the values.
        private void Reallocate()
        {
            if (TypeOfValue != 0)
                Values = new byte[m_size * TypeOfValue.GetByteSize()];
        }

        #endregion
    }
}
