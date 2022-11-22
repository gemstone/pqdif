//******************************************************************************************************
//  ScalarElement.cs - Gbtc
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
    /// Represents an <see cref="Element"/> which is a single value in a
    /// PQDIF file. Scalar elements are part of the physical structure of
    /// a PQDIF file. They exist within the body of a <see cref="Record"/>
    /// (contained by a <see cref="CollectionElement"/>).
    /// </summary>
    public class ScalarElement : Element
    {
        #region [ Members ]

        // Fields
        private byte[] m_value;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Creates a new instance of the <see cref="ScalarElement"/> class.
        /// </summary>
        public ScalarElement()
        {
            m_value = new byte[16];
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets the type of the element.
        /// Returns <see cref="ElementType.Scalar"/>.
        /// </summary>
        public override ElementType TypeOfElement
        {
            get
            {
                return ElementType.Scalar;
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Gets the value of the scalar as the physical type defined
        /// by <see cref="Element.TypeOfValue"/> and returns it as a generic
        /// <see cref="object"/>.
        /// </summary>
        /// <returns>The value of the scalar.</returns>
        public object Get()
        {
            return TypeOfValue switch
            {
                PhysicalType.Boolean1 => m_value[0] != 0,
                PhysicalType.Boolean2 => GetInt2() != 0,
                PhysicalType.Boolean4 => GetInt4() != 0,
                PhysicalType.Char1 => Encoding.ASCII.GetString(m_value, 0, 1),
                PhysicalType.Char2 => Encoding.Unicode.GetString(m_value, 0, 2),
                PhysicalType.Integer1 => (sbyte)m_value[0],
                PhysicalType.Integer2 => GetInt2(),
                PhysicalType.Integer4 => GetInt4(),
                PhysicalType.UnsignedInteger1 => m_value[0],
                PhysicalType.UnsignedInteger2 => GetUInt2(),
                PhysicalType.UnsignedInteger4 => GetUInt4(),
                PhysicalType.Real4 => GetReal4(),
                PhysicalType.Real8 => GetReal8(),
                PhysicalType.Complex8 => GetComplex8(),
                PhysicalType.Complex16 => GetComplex16(),
                PhysicalType.Timestamp => GetTimestamp(),
                PhysicalType.Guid => GetGuid(),
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        /// <summary>                
        /// Sets the value at the given index as the physical type defined by <see cref="Element.TypeOfValue"/>.
        /// </summary>
        /// <param name="value">The new value to be stored.</param>
        public void Set(object value)
        {
            char c;
            byte[] bytes;

            switch (TypeOfValue)
            {
                case PhysicalType.Boolean1:
                    SetUInt1(Convert.ToBoolean(value) ? (byte)1 : (byte)0);
                    break;

                case PhysicalType.Boolean2:
                    SetInt2(Convert.ToBoolean(value) ? (short)1 : (short)0);
                    break;

                case PhysicalType.Boolean4:
                    SetInt4(Convert.ToBoolean(value) ? 1 : 0);
                    break;

                case PhysicalType.Char1:
                    c = Convert.ToChar(value);
                    bytes = Encoding.ASCII.GetBytes(c.ToString());
                    SetUInt1(bytes[0]);
                    break;

                case PhysicalType.Char2:
                    c = Convert.ToChar(value);
                    bytes = Encoding.Unicode.GetBytes(c.ToString());
                    SetInt2(BitConverter.ToInt16(bytes, 0));
                    break;

                case PhysicalType.Integer1:
                    SetInt1(Convert.ToSByte(value));
                    break;

                case PhysicalType.Integer2:
                    SetInt2(Convert.ToInt16(value));
                    break;

                case PhysicalType.Integer4:
                    SetInt4(Convert.ToInt32(value));
                    break;

                case PhysicalType.UnsignedInteger1:
                    SetUInt1(Convert.ToByte(value));
                    break;

                case PhysicalType.UnsignedInteger2:
                    SetUInt2(Convert.ToUInt16(value));
                    break;

                case PhysicalType.UnsignedInteger4:
                    SetUInt4(Convert.ToUInt32(value));
                    break;

                case PhysicalType.Real4:
                    SetReal4(Convert.ToSingle(value));
                    break;

                case PhysicalType.Real8:
                    SetReal8(Convert.ToDouble(value));
                    break;

                case PhysicalType.Complex8:
                    SetComplex8((Complex)value);
                    break;

                case PhysicalType.Complex16:
                    SetComplex16((Complex)value);
                    break;

                case PhysicalType.Timestamp:
                    SetTimestamp(Convert.ToDateTime(value));
                    break;

                case PhysicalType.Guid:
                    SetGuid((Guid)value);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Gets the value of this scalar as an 8-bit unsigned integer.
        /// </summary>
        /// <returns>The value as an 8-bit unsigned integer.</returns>
        public ushort GetUInt1()
        {
            return m_value[0];
        }

        /// <summary>
        /// Sets the value of this scalar as an 8-bit unsigned integer.
        /// </summary>
        /// <param name="value">The new value as an 8-bit unsigned integer.</param>
        public void SetUInt1(byte value)
        {
            m_value[0] = value;
        }

        /// <summary>
        /// Gets the value of this scalar as an 8-bit signed integer.
        /// </summary>
        /// <returns>The value as an 8-bit signed integer.</returns>
        public short GetInt1()
        {
            return (sbyte)m_value[0];
        }

        /// <summary>
        /// Sets the value of this scalar as an 8-bit signed integer.
        /// </summary>
        /// <param name="value">The new value as an 8-bit signed integer.</param>
        public void SetInt1(sbyte value)
        {
            m_value[0] = (byte)value;
        }

        /// <summary>
        /// Gets the value of this scalar as a 16-bit unsigned integer.
        /// </summary>
        /// <returns>The value as a 16-bit unsigned integer.</returns>
        public ushort GetUInt2()
        {
            if (BitConverter.IsLittleEndian)
                return BitConverter.ToUInt16(m_value, 0);

            Span<byte> value = m_value.AsSpan().Slice(0, sizeof(ushort));
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
        /// Sets the value of this scalar as a 16-bit unsigned integer.
        /// </summary>
        /// <param name="value">The new value as a 16-bit unsigned integer.</param>
        public void SetUInt2(ushort value)
        {
            Span<byte> span = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
                span.Reverse();

            span.CopyTo(m_value);
        }

        /// <summary>
        /// Gets the value of this scalar as a 16-bit signed integer.
        /// </summary>
        /// <returns>The value as a 16-bit signed integer.</returns>
        public short GetInt2()
        {
            if (BitConverter.IsLittleEndian)
                return BitConverter.ToInt16(m_value, 0);

            Span<byte> value = m_value.AsSpan().Slice(0, sizeof(short));
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
        /// Sets the value of this scalar as a 16-bit signed integer.
        /// </summary>
        /// <param name="value">The new value as a 16-bit signed integer.</param>
        public void SetInt2(short value)
        {
            Span<byte> span = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
                span.Reverse();

            span.CopyTo(m_value);
        }

        /// <summary>
        /// Gets the value of this scalar as a 32-bit unsigned integer.
        /// </summary>
        /// <returns>The value as a 32-bit unsigned integer.</returns>
        public uint GetUInt4()
        {
            if (BitConverter.IsLittleEndian)
                return BitConverter.ToUInt32(m_value, 0);

            Span<byte> value = m_value.AsSpan().Slice(0, sizeof(uint));
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
        /// Sets the value of this scalar as a 32-bit unsigned integer.
        /// </summary>
        /// <param name="value">The new value as a 32-bit unsigned integer.</param>
        public void SetUInt4(uint value)
        {
            Span<byte> span = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
                span.Reverse();

            span.CopyTo(m_value);
        }

        /// <summary>
        /// Gets the value of this scalar as a 32-bit signed integer.
        /// </summary>
        /// <returns>The value as a 32-bit signed integer.</returns>
        public int GetInt4()
        {
            if (BitConverter.IsLittleEndian)
                return BitConverter.ToInt32(m_value, 0);

            Span<byte> value = m_value.AsSpan().Slice(0, sizeof(int));
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
        /// Sets the value of this scalar as a 32-bit signed integer.
        /// </summary>
        /// <param name="value">The new value as a 32-bit signed integer.</param>
        public void SetInt4(int value)
        {
            Span<byte> span = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
                span.Reverse();

            span.CopyTo(m_value);
        }

        /// <summary>
        /// Gets the value of this scalar as a 4-byte boolean.
        /// </summary>
        /// <returns>The value as a 4-byte boolean.</returns>
        public bool GetBool4()
        {
            if (BitConverter.IsLittleEndian)
                return BitConverter.ToInt32(m_value, 0) != 0;

            Span<byte> value = m_value.AsSpan().Slice(0, sizeof(int));
            Span<byte> copy = new byte[value.Length];
            value.CopyTo(copy);
            copy.Reverse();

        #if NETSTANDARD2_0
            return BitConverter.ToInt32(copy.ToArray(), 0) != 0;
        #else
            return BitConverter.ToInt32(copy) != 0;
        #endif
        }

        /// <summary>
        /// Sets the value of this scalar as a 4-byte boolean.
        /// </summary>
        /// <param name="value">The new value as a 4-byte boolean.</param>
        public void SetBool4(bool value)
        {
            Span<byte> span = BitConverter.GetBytes(value ? 1 : 0);

            if (!BitConverter.IsLittleEndian)
                span.Reverse();

            span.CopyTo(m_value);
        }

        /// <summary>
        /// Gets the value of this scalar as a 32-bit floating point number.
        /// </summary>
        /// <returns>The value as a 32-bit floating point number.</returns>
        public float GetReal4()
        {
            if (BitConverter.IsLittleEndian)
                return BitConverter.ToSingle(m_value, 0);

            Span<byte> value = m_value.AsSpan().Slice(0, sizeof(int));
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
        /// Sets the value of this scalar as a 32-bit floating point number.
        /// </summary>
        /// <param name="value">The new value as a 32-bit floating point number.</param>
        public void SetReal4(float value)
        {
            Span<byte> span = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
                span.Reverse();

            span.CopyTo(m_value);
        }

        /// <summary>
        /// Gets the value of this scalar as a 64-bit floating point number.
        /// </summary>
        /// <returns>The value as a 64-bit floating point number.</returns>
        public double GetReal8()
        {
            if (BitConverter.IsLittleEndian)
                return BitConverter.ToDouble(m_value, 0);

            Span<byte> value = m_value.AsSpan().Slice(0, sizeof(int));
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
        /// Sets the value of this scalar as a 64-bit floating point number.
        /// </summary>
        /// <param name="value">The new value as a 64-bit floating point number.</param>
        public void SetReal8(double value)
        {
            Span<byte> span = BitConverter.GetBytes(value);

            if (!BitConverter.IsLittleEndian)
                span.Reverse();

            span.CopyTo(m_value);
        }

        /// <summary>
        /// Gets the value of this scalar as an 8-byte complex number.
        /// </summary>
        /// <returns>The value as an 8-byte complex number.</returns>
        public Complex GetComplex8()
        {
            int size = sizeof(float);
            Span<byte> realSpan = m_value.AsSpan().Slice(0, size);
            Span<byte> imaginarySpan = m_value.AsSpan().Slice(size, size);

            if (!BitConverter.IsLittleEndian)
            {
                Span<byte> realSwap = new byte[size];
                realSpan.CopyTo(realSwap);
                realSwap.Reverse();
                realSpan = realSwap;

                Span<byte> imaginarySwap = new byte[size];
                imaginarySpan.CopyTo(imaginarySwap);
                imaginarySwap.Reverse();
                imaginarySpan = imaginarySwap;
            }

        #if NETSTANDARD2_0
            double real = BitConverter.ToSingle(realSpan.ToArray(), 0);
            double imaginary = BitConverter.ToSingle(imaginarySpan.ToArray(), 0);
        #else
            double real = BitConverter.ToSingle(realSpan);
            double imaginary = BitConverter.ToSingle(imaginarySpan);
        #endif
            
            return new Complex(real, imaginary);
        }

        /// <summary>
        /// Sets the value of this scalar as an 8-byte complex number.
        /// </summary>
        /// <param name="value">The new value as an 8-byte complex number.</param>
        public void SetComplex8(Complex value)
        {
            Span<byte> real = BitConverter.GetBytes((float)value.Real);
            Span<byte> imaginary = BitConverter.GetBytes((float)value.Imaginary);

            if (!BitConverter.IsLittleEndian)
            {
                real.Reverse();
                imaginary.Reverse();
            }

            real.CopyTo(m_value);
            imaginary.CopyTo(m_value.AsSpan().Slice(real.Length));
        }

        /// <summary>
        /// Gets the value of this scalar as a 16-byte complex number.
        /// </summary>
        /// <returns>The value as a 16-byte complex number.</returns>
        public Complex GetComplex16()
        {
            int size = sizeof(double);
            Span<byte> realSpan = m_value.AsSpan().Slice(0, size);
            Span<byte> imaginarySpan = m_value.AsSpan().Slice(size, size);

            if (!BitConverter.IsLittleEndian)
            {
                Span<byte> realSwap = new byte[size];
                realSpan.CopyTo(realSwap);
                realSwap.Reverse();
                realSpan = realSwap;

                Span<byte> imaginarySwap = new byte[size];
                imaginarySpan.CopyTo(imaginarySwap);
                imaginarySwap.Reverse();
                imaginarySpan = imaginarySwap;
            }

        #if NETSTANDARD2_0
            double real = BitConverter.ToDouble(realSpan.ToArray(), 0);
            double imaginary = BitConverter.ToDouble(imaginarySpan.ToArray(), 0);
        #else
            double real = BitConverter.ToDouble(realSpan);
            double imaginary = BitConverter.ToDouble(imaginarySpan);
        #endif

            return new Complex(real, imaginary);
        }

        /// <summary>
        /// Sets the value of this scalar as a 16-byte complex number.
        /// </summary>
        /// <param name="value">The new value as a 16-byte complex number.</param>
        public void SetComplex16(Complex value)
        {
            Span<byte> real = BitConverter.GetBytes(value.Real);
            Span<byte> imaginary = BitConverter.GetBytes(value.Imaginary);

            if (!BitConverter.IsLittleEndian)
            {
                real.Reverse();
                imaginary.Reverse();
            }

            real.CopyTo(m_value);
            imaginary.CopyTo(m_value.AsSpan().Slice(real.Length));
        }

        /// <summary>
        /// Gets the value of this scalar as a globally unique identifier.
        /// </summary>
        /// <returns>The value as a globally unique identifier.</returns>
        public Guid GetGuid()
        {
            return new Guid(m_value);
        }

        /// <summary>
        /// Sets the value of this scalar as a globally unique identifier.
        /// </summary>
        /// <param name="value">The new value as a globally unique identifier.</param>
        public void SetGuid(Guid value)
        {
            m_value = value.ToByteArray();
        }

        /// <summary>
        /// Gets the value of this scalar as <see cref="DateTime"/>.
        /// </summary>
        /// <returns>The value of this scalar as a <see cref="DateTime"/>.</returns>
        public DateTime GetTimestamp()
        {
            Span<byte> daySpan = m_value.AsSpan().Slice(0, sizeof(uint));
            Span<byte> secondSpan = m_value.AsSpan().Slice(daySpan.Length, sizeof(double));

            if (!BitConverter.IsLittleEndian)
            {
                daySpan.Reverse();
                secondSpan.Reverse();
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
        /// Sets the value of this scalar as a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="value">The new value of this scalar as a <see cref="DateTime"/>.</param>
        public void SetTimestamp(DateTime value)
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

            daySpan.CopyTo(m_value);
            secondSpan.CopyTo(m_value.AsSpan().Slice(daySpan.Length));
        }

        /// <summary>
        /// Gets the raw bytes of the value that this scalar represents.
        /// </summary>
        /// <returns>The value in bytes.</returns>
        public byte[] GetValue()
        {
            int size = TypeOfValue.GetByteSize();
            byte[] copy = new byte[size];
            Buffer.BlockCopy(m_value, 0, copy, 0, size);
            return copy;
        }

        /// <summary>
        /// Sets the raw bytes of the value that this scalar represents.
        /// </summary>
        /// <param name="value">The array containing the bytes.</param>
        /// <param name="offset">The offset into the array at which the value starts.</param>
        public void SetValue(byte[] value, int offset)
        {
            Buffer.BlockCopy(value, offset, m_value, 0, TypeOfValue.GetByteSize());
        }

        /// <summary>
        /// Returns a string representation of the scalar.
        /// </summary>
        /// <returns>A string representation of the scalar.</returns>
        public override string ToString()
        {
            return $"Scalar -- Type: {TypeOfValue}, Tag: {TagOfElement}";
        }

        #endregion
    }
}
