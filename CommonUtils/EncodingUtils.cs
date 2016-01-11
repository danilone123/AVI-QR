using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

namespace SACommon
{
    public class TextEncoder
    {
        private const string CLASSNAME = "TextEncoder";
        private const long BaseTime = 63082281600L;

        public TextEncoder()
        {

        }

        private void ReverseChunk(char[] toRotate)
        {
            byte ByteChar;
            for (int i = 0; i < 5; i++)
            {
                ByteChar = (byte)(((int)(toRotate[i] & 15) << 4) | ((int)(toRotate[i] & 240) >> 4));
                toRotate[i] = (char)(ByteChar ^ 255);
            }
        }

        private char ToBase32Char(long Long)
        {
            if (Long < 5)
            {
                return ((char)('j' + Long));
            }
            else if (Long < 13)
            {
                return ((char)('2' + (Long - 5)));
            }
            else if (Long < 24)
            {
                return ((char)('p' + (Long - 13)));
            }
            else if (Long < 32)
            {
                return ((char)('a' + (Long - 24)));
            }
            else
            {
                return ('0');
            }
        }

        private char[] ToBase32(char[] chunk)
        {
            long Value;
            char[] RetVal = new char[8];

            Value = chunk[0];
            RetVal[0] = ToBase32Char(Value & 31);

            Value >>= 5;
            Value |= (uint)((chunk[1] << 3));

            RetVal[1] = ToBase32Char(Value & 31);

            Value >>= 5;
            RetVal[2] = ToBase32Char(Value & 31);

            Value >>= 5;
            Value |= (uint)(chunk[2] << 1);
            RetVal[3] = ToBase32Char(Value & 31);

            Value >>= 5;
            Value |= (uint)(chunk[3] << 4);
            RetVal[4] = ToBase32Char(Value & 31);

            Value >>= 5;
            RetVal[5] = ToBase32Char(Value & 31);

            Value >>= 5;
            Value |= (uint)(chunk[4] << 2);
            RetVal[6] = ToBase32Char(Value & 31);

            Value >>= 5;
            RetVal[7] = ToBase32Char(Value & 31);
            return (RetVal);
        }

        private long CalcChecksum(long checksum, char[] chunk)
        {
            int idx;
            long retval = checksum;

            for (idx = 0; idx < 5; idx++)
            {
                retval ^= ((long)(chunk[idx]) << (idx * 6));
            }
            return (retval);
        }

        private long CharArrayToLong(char[] CharArray)
        {
            long Out;
            Out = (long)CharArray[0];
            Out |= ((long)CharArray[1] << 8);
            Out |= ((long)CharArray[2] << 16);
            Out |= ((long)CharArray[3] << 24);
            return (Out);
        }

        private char[] LongToCharArray(long Long)
        {
            char[] CharArray = new char[5];
            CharArray[0] = (char)(Long & 255);
            Long >>= 8;
            CharArray[1] = (char)(Long & 255);
            Long >>= 8;
            CharArray[2] = (char)(Long & 255);
            Long >>= 8;
            CharArray[3] = (char)(Long & 255);
            Long >>= 8;
            CharArray[4] = (char)(Long & 255);

            return (CharArray);
        }

        private char[] FromBase32(char[] Inc)
        {
            char[] chunk = new char[5];
            long Value;

            Value = FromBase32Char(Inc[7]);
            Value <<= 5;

            Value |= FromBase32Char(Inc[6]);
            chunk[4] = (char)((Value >> 2) & 255);
            Value <<= 5;

            Value |= FromBase32Char(Inc[5]);
            Value <<= 5;

            Value |= FromBase32Char(Inc[4]);
            chunk[3] = (char)((Value >> 4) & 255);
            Value <<= 5;

            Value |= FromBase32Char(Inc[3]);
            chunk[2] = (char)((Value >> 1) & 255);
            Value <<= 5;

            Value |= FromBase32Char(Inc[2]);
            Value <<= 5;

            Value |= FromBase32Char(Inc[1]);
            chunk[1] = (char)((Value >> 3) & 255);
            Value <<= 5;

            Value |= FromBase32Char(Inc[0]);
            chunk[0] = (char)(Value & 255);

            return (chunk);
        }

        private long FromBase32Char(char CharValue)
        {
            if (CharValue >= 'j' && CharValue <= 'n')
            {
                return (CharValue - 'j');
            }
            else if (CharValue >= '2' && CharValue <= '9')
            {
                return ((CharValue - '2') + 5);
            }
            else if (CharValue >= 'p' && CharValue <= 'z')
            {
                return ((CharValue - 'p') + 13);
            }
            else if (CharValue >= 'a' && CharValue <= 'h')
            {
                return ((CharValue - 'a') + 24);
            }
            return (0);
        }

        public string Decode(string EncodedTxtString, long Expire)
        {
            char[] buff = new char[6];
            char[] chunk = new char[5];
            char[] head = new char[5];
            char[] tail = new char[5];
            char[] temp = new char[8];
            long EncodedTxtLen = EncodedTxtString.Length;
            long checksum = 0;
            StringBuilder strBldr = new StringBuilder(512);
            int dx = 0;

            buff[5] = (char)0;

            if (((EncodedTxtLen % 8) != 0) || (EncodedTxtLen < 16))
            {
                throw (new Exception("Checksum Invalid!"));
            }

            if (EncodedTxtLen > 432)
            {
                dx = 8;
            }

            ArrayChar IncAsChar = new ArrayChar((int)EncodedTxtLen);
            IncAsChar.Add(EncodedTxtString.ToCharArray());

            head = FromBase32(IncAsChar.Read(8));
            long time = CharArrayToLong(head);
            checksum = CalcChecksum(checksum, head);
            EncodedTxtLen -= 8;

            while (EncodedTxtLen > (8 + dx))
            {
                temp = IncAsChar.Read(8);
                chunk = FromBase32(temp);
                checksum = CalcChecksum(checksum, chunk);
                ReverseChunk(chunk);
                EncodedTxtLen -= 8;
                strBldr.Append(chunk);
            }

            tail = FromBase32(IncAsChar.Read(8));
            EncodedTxtLen -= 8;
            int leng = (int)tail[4];
            long CheckSum = CharArrayToLong(tail);

            if (leng == 0 && EncodedTxtLen > 0)
            {
                tail = FromBase32(IncAsChar.Read(8));
                leng = (int)CharArrayToLong(tail);
            }

            if (CheckSum != checksum)
            {
                throw (new Exception("Checksum Invalid!"));
            }
            if (System.DateTime.Now.Ticks > (long)(time + Expire + BaseTime) * 10000000L)
            {
                throw (new Exception("Time expired!"));
            }

            return (strBldr.ToString(0, leng));
        }

        public string Encode(string PlainTextString)
        {
            char[] Head = new char[5];
            char[] Tail = new char[5];
            char[] TempChar8 = new char[8];
            char[] TempChar = new char[5];
            long Time;
            long CheckSum = 0;
            long LongReverse = 0;
            int PlainTxtLen = PlainTextString.Length;
            int GroupsOf5Chars = (int)Math.Floor((double)(PlainTxtLen / 5));
            int EncTxtStrLen;
            int dx = (PlainTxtLen > 255) ? 8 : 0;
            ArrayChar ArrayOut = new ArrayChar(8 * GroupsOf5Chars + 24 + dx);
            String Out;

            Time = ((long)DateTime.Now.Ticks);
            Time = Time / 10000000L;
            Time = Time - BaseTime;
            Head = LongToCharArray(Time);
            TempChar8 = ToBase32(Head);
            ArrayOut.Add(TempChar8);
            CheckSum = CalcChecksum(CheckSum, Head);

            for (int Idx = 0; Idx < GroupsOf5Chars; Idx++)
            {
                TempChar = PlainTextString.ToCharArray(Idx * 5, 5);
                ReverseChunk(TempChar);
                LongReverse = CharArrayToLong(TempChar);
                TempChar8 = ToBase32(TempChar);
                ArrayOut.Add(TempChar8);
                CheckSum = CalcChecksum(CheckSum, TempChar);
            }

            EncTxtStrLen = PlainTextString.Length % 5;

            for (int Idx = 0; Idx < EncTxtStrLen; Idx++)
            {
                TempChar[Idx] = PlainTextString.ToCharArray()[((GroupsOf5Chars) * 5) + Idx];
            }
            for (int Idx = EncTxtStrLen; Idx < 5; Idx++)
            {
                TempChar[Idx] = (char)(5 - Idx);
            }

            ReverseChunk(TempChar);
            TempChar8 = ToBase32(TempChar);
            ArrayOut.Add(TempChar8);
            CheckSum = CalcChecksum(CheckSum, TempChar);

            Tail = LongToCharArray(CheckSum);
            if (PlainTxtLen > 255)
            {
                Tail[4] = (char)0;
            }
            else
            {
                Tail[4] = (char)PlainTxtLen;
            }
            TempChar8 = ToBase32(Tail);
            ArrayOut.Add(TempChar8);

            if (PlainTxtLen > 255)
            {
                Tail = LongToCharArray(PlainTxtLen);
                TempChar8 = ToBase32(Tail);
                ArrayOut.Add(TempChar8);
            }
            Out = new String(ArrayOut.GetBuffer());
            return (Out);
        }

    }

    public class ArrayChar
    {
        private char[] InternalBuffer;
        private int InternalPos;
        private int ReadPos;

        public ArrayChar()
        {
            InternalBuffer = new char[1];
            InternalPos = 0;
            ReadPos = 0;
        }

        public ArrayChar(int Capacity)
        {
            InternalBuffer = new char[Capacity];
            InternalPos = 0;
            ReadPos = 0;
        }

        public void Add(char[] AppendChunk)
        {
            int PosChunck = AppendChunk.Length;
            int Idx = 0;

            for (Idx = 0; Idx < PosChunck; Idx++)
            {
                InternalBuffer[Idx + InternalPos] = AppendChunk[Idx];
            }
            InternalPos += Idx;
        }

        public char[] GetBuffer()
        {
            return (InternalBuffer);
        }

        public char[] Read(int Count)
        {
            char[] TemBuff = new char[Count];
            for (int Idx = 0; Idx < Count; Idx++)
            {
                TemBuff[Idx] = InternalBuffer[ReadPos + Idx];
            }
            ReadPos += Count;
            return (TemBuff);
        }

        public char[] ReadFromBottom(int Count)
        {
            char[] TemBuff = new char[Count];
            int AuxPos = InternalBuffer.Length - Count;
            for (int Idx = 0; Idx < Count; Idx++)
            {
                TemBuff[Idx] = InternalBuffer[AuxPos + Idx];
            }
            return (TemBuff);
        }
    }
}

