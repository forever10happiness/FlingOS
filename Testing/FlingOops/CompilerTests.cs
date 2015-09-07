﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NoGCAttribute = Drivers.Compiler.Attributes.NoGCAttribute;
using Log = FlingOops.BasicConsole;

namespace FlingOops
{
    /// <summary>
    /// This class contains behavioural tests of the compiler.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class contains what are intended to be behavioural tests of the FlingOS Compiler
    /// IL Op to ASM ops conversions. Unfortunately, even the most basic test has to use a
    /// significant number of IL ops just to be able to output something useful.
    /// </para>
    /// <para>
    /// As a result, the tests provided cannot be run in an automated fashion. The compiler
    /// tester/developer will need to select which tests to run, execute them and observe
    /// the output to determine if it has worked or not. If not, manual debugging will be
    /// required.
    /// </para>
    /// <para>
    /// Instead of altering the test itself, a copy should be made to an architecture-specific
    /// test class and modifications be made to the copy. Once testing is complete and the bug
    /// has been fixed, it should be documented thoroughly for future reference. The architecture
    /// specific test class should then be removed.
    /// </para>
    /// <para>
    /// For MIPS UART_Num is set to 0 for UBoot serial booting and is set to 4 for UBS OTG booting.
    /// </para>
    /// </remarks>

    /// <summary>
    /// Test struct for testing structs.
    /// </summary>
    public struct AStruct
    {
        public byte a;      // 1 byte - 1 byte on heap
        public short b;     // 2 bytes - 2 bytes on heap
        public int c;       // 4 bytes - 4 bytes on heap
        public long d;      // 8 bytes - 8 bytes on heap
                            // Total : 15 bytes
    }

    public static class CompilerTests
    {
        /// <summary>
        /// Executes all the tests in the CompilerTests class.
        /// </summary>
        [NoGC]
        public static void RunTests()
        {
            #region Struct calls

            Log.WriteLine("---Struct:");
            Test_Sizeof_Struct();
            Test_Instance_Struct();
            Log.WriteLine(" ");

            #endregion

            #region Variables and pointers calls

            Log.WriteLine("---Variables and pointers:");
            Test_Locals_And_Pointers();
            Log.WriteLine(" ");

            #endregion

            #region Modulus calls

            Log.WriteLine("---Modulus:");
            Log.WriteLine("  Unsigned");
            Test_Mod_UInt32_9_UInt32_3();
            Test_Mod_UInt32_10_UInt32_3();
            Log.WriteLine("  Signed");
            Test_Mod_Int32_9_Int32_3();
            Test_Mod_Int32_9_Int32_Neg3();
            Test_Mod_Int32_10_Int32_3();
            Test_Mod_Int32_10_Int32_Neg3();
            Test_Mod_Int32_Neg9_Int32_3();
            Test_Mod_Int32_Neg9_Int32_Neg3();
            Test_Mod_Int32_Neg10_Int32_3();
            Test_Mod_Int32_Neg10_Int32_Neg3();
            Log.WriteLine(" ");

            #endregion

            #region Division calls

            Log.WriteLine("---Division:");
            Log.WriteLine("  Unsigned");
            Test_Div_UInt32_9_UInt32_3();
            Test_Div_UInt32_10_UInt32_3();
            Log.WriteLine("  Signed");
            Test_Div_Int32_9_Int32_3();
            Test_Div_Int32_9_Int32_Neg3();
            Test_Div_Int32_10_Int32_3();
            Test_Div_Int32_10_Int32_Neg3();
            Test_Div_Int32_Neg9_Int32_3();
            Test_Div_Int32_Neg9_Int32_Neg3();
            Test_Div_Int32_Neg10_Int32_3();
            Test_Div_Int32_Neg10_Int32_Neg3();
            Log.WriteLine(" ");

            #endregion

            #region Subtraction calls

            Log.WriteLine("---Subtraction:");
            Log.WriteLine(" 32-32");
            Log.WriteLine("  Unsigned");
            Test_Sub_UInt32_9_UInt32_4();
            Log.WriteLine("  Signed");
            Test_Sub_Int32_9_Int32_4();
            Test_Sub_Int32_9_Int32_Neg4();
            Test_Sub_Int32_Neg9_Int32_4();
            Test_Sub_Int32_Neg9_Int32_Neg4();
            Log.WriteLine(" 64-32");
            Log.WriteLine("  Unsigned");
            Test_Sub_UInt64_LargestPos_UInt32_4();
            Log.WriteLine("  Signed");
            Test_Sub_Int64_LargestPos_Int32_4();
            Test_Sub_Int64_LargestNeg_Int32_4();
            Test_Sub_Int64_Zero_Int32_4();
            Test_Sub_Int64_Zero_Int32_LargestPos();
            Test_Sub_Int64_Zero_Int32_LargestNeg();
            Log.WriteLine(" 64-64");
            Log.WriteLine("  Unsigned");
            Test_Sub_UInt64_Large_UInt64_Large();
            Log.WriteLine("  Signed");
            Test_Sub_Int64_LargePos_Int64_4();
            Test_Sub_Int64_LargePos_Int64_LargePos();
            Test_Sub_Int64_LargePos_Int64_LargeNeg();
            Test_Sub_Int64_LargestNeg_Int64_1();
            Test_Sub_Int64_LargeNeg_Int64_LargeNeg();
            Test_Sub_Int64_Zero_Int64_4();
            Test_Sub_Int64_Zero_Int64_LargePos();
            Test_Sub_Int64_Zero_Int64_LargeNeg();
            Log.WriteLine(" ");

            #endregion

            #region Right shift calls

            Log.WriteLine("---Right shift:");
            Log.WriteLine(" 32");
            Log.WriteLine("  Unsigned");
            Test_RShift_UInt32_Small_Int32_6();
            Test_RShift_UInt32_Largest_Int32_6();
            Log.WriteLine("  Signed");
            Test_RShift_Int32_SmallPos_Int32_6();
            Test_RShift_Int32_SmallNeg_Int32_6();
            Test_RShift_Int32_LargestPos_Int32_6();
            Test_RShift_Int32_LargestNeg_Int32_6();
            Log.WriteLine(" 64");
            Log.WriteLine("  Unsigned");
            Log.WriteLine("   dist<32");
            Test_RShift_UInt64_Large_Int32_10();
            Test_RShift_UInt64_Largest_Int32_10();
            Log.WriteLine("   dist>=32");
            Test_RShift_UInt64_Largest_Int32_63();
            Log.WriteLine("  Signed");
            Log.WriteLine("   dist<32");
            Test_RShift_Int64_LargeNeg_Int32_6();
            Log.WriteLine("   dist>=32");
            Test_RShift_Int64_LargestPos_Int32_40();
            Test_RShift_Int64_LargestNeg_Int32_40();
            Test_RShift_Int64_LargeNeg_Int32_40();
            Test_RShift_Int64_Neg1_Int32_63();
            Log.WriteLine(" ");

            #endregion

            #region Left shift calls

            Log.WriteLine("---Left shift:");
            Log.WriteLine(" 32");
            Log.WriteLine("  Unsigned");
            Test_LShift_UInt32_Small_Int32_6();
            Test_LShift_UInt32_Largest_Int32_6();
            Log.WriteLine("  Signed");
            Test_LShift_Int32_SmallPos_Int32_6();
            Test_LShift_Int32_SmallNeg_Int32_6();
            Test_LShift_Int32_LargestPos_Int32_6();
            Test_LShift_Int32_LargestNeg_Int32_6();
            Log.WriteLine(" 64");
            Log.WriteLine("  Unsigned");
            Log.WriteLine("   dist<32");
            Test_LShift_UInt64_Large_Int32_2();
            Test_LShift_UInt64_Largest_Int32_10();
            Log.WriteLine("   dist>=32");
            Test_LShift_UInt64_Largest_Int32_63();
            Log.WriteLine("  Signed");
            Log.WriteLine("   dist<32");
            Test_LShift_Int64_LargeNeg_Int32_6();
            Log.WriteLine("   dist>=32");
            Test_LShift_Int64_LargestPos_Int32_40();
            Test_LShift_Int64_LargestNeg_Int32_40();
            Test_LShift_Int64_LargeNeg_Int32_40();
            Test_LShift_Int64_Neg1_Int32_63();
            Log.WriteLine(" ");



            #endregion

            #region Negation calls

            Log.WriteLine("---Negation:");
            Log.WriteLine(" 32");
            Log.WriteLine("  Unsigned");
            Log.WriteLine("UInt32 cannot be negated into Int32, only into Int64 in C#.");
            Test_Neg_UInt32_Small_Int64();
            Test_Neg_UInt32_Largest_Int64();
            Log.WriteLine("  Signed");
            Test_Neg_Int32_SmallPos_Int32();
            Test_Neg_Int32_SmallNeg_Int32();
            Test_Neg_Int32_LargePos_Int64();
            Test_Neg_Int32_LargeNeg_Int64();
            Log.WriteLine(" 64");
            Log.WriteLine("  Unsigned");
            Log.WriteLine("UInt64 cannot be negated in C#.");
            Log.WriteLine("  Signed");
            Test_Neg_Int64_LargePos_Int64();
            Test_Neg_Int64_LargeNeg_Int64();
            Test_Neg_Int64_LargestPos_Int64();
            Test_Neg_Int64_LargestNeg_Int64();
            Log.WriteLine(" ");

            #endregion

            #region Not calls

            Log.WriteLine("---Not:");
            Log.WriteLine(" 32");
            Log.WriteLine("  Unsigned");
            Log.WriteLine("UInt32 cannot be not-ed into Int32, only into Int64 in C#.");
            Test_Not_UInt32_Small_Int64();
            Test_Not_UInt32_Largest_Int64();
            Log.WriteLine("  Signed");
            Test_Not_Int32_SmallPos_Int32();
            Test_Not_Int32_SmallNeg_Int32();
            Test_Not_Int32_LargePos_Int64();
            Test_Not_Int32_LargeNeg_Int64();
            Log.WriteLine(" 64");
            Log.WriteLine("  Unsigned");
            Test_Not_UInt64_Smallest_UInt64();
            Test_Not_UInt64_Largest_UInt64();
            Log.WriteLine("  Signed");
            Test_Not_Int64_LargePos_Int64();
            Test_Not_Int64_LargeNeg_Int64();
            Test_Not_Int64_LargestPos_Int64();
            Test_Not_Int64_LargestNeg_Int64();
            Log.WriteLine(" ");

            #endregion

            #region Addition calls

            Log.WriteLine("---Addition:");
            Log.WriteLine(" 32-32");
            Log.WriteLine("  Unsigned");
            Test_Add_UInt32_Zero_UInt32_Zero();
            Test_Add_UInt32_9_UInt32_4();
            Log.WriteLine("  Signed");
            Test_Add_Int32_9_Int32_4();
            Test_Add_Int32_9_Int32_Neg4();
            Test_Add_Int32_Neg9_Int32_4();
            Test_Add_Int32_Neg9_Int32_Neg4();
            Log.WriteLine(" 64-32");
            Log.WriteLine("  Unsigned");
            Test_Add_UInt64_LargestPos_UInt32_4();
            Log.WriteLine("  Signed");
            Test_Add_Int64_LargestPos_Int32_4();
            Test_Add_Int64_LargestNeg_Int32_4();
            Test_Add_Int64_Zero_Int32_LargestNeg();
            Log.WriteLine(" 64-64");
            Log.WriteLine("  Unsigned");
            Test_Add_UInt64_Large_UInt64_Large();
            Log.WriteLine("  Signed");
            Test_Add_Int64_LargePos_Int64_4();
            Test_Add_Int64_LargePos_Int64_LargePos();
            Test_Add_Int64_LargePos_Int64_LargeNeg();
            Test_Add_Int64_LargestNeg_Int64_Neg1();
            Test_Add_Int64_LargeNeg_Int64_LargeNeg();
            Test_Add_Int64_Zero_Int64_Neg4();
            Test_Add_Int64_Zero_Int64_LargePos();
            Test_Add_Int64_Zero_Int64_LargeNeg();
            Log.WriteLine(" ");

            #endregion

            #region Switch calls

            Log.WriteLine("---Switch:");
            Log.WriteLine(" Integers");
            Test_Switch_Int32_Case_0();
            Test_Switch_Int32_Case_1();
            Test_Switch_Int32_Case_2();
            Test_Switch_Int32_Case_Default();
            Test_Switch_Int32_Case_0_Ret_NoValue();
            Log.WriteLine("  Successfully returned from Test_Switch_Int32_Case_0_Ret_NoValue()");
            Test_Switch_Int32_Case_0_Ret_IntValue();
            Log.WriteLine("  Successfully returned from Test_Switch_Int32_Case_0_Ret_IntValue()");
            Test_Switch_Int32_Case_0_Ret_StringValue();
            Log.WriteLine("  Successfully returned from Test_Switch_Int32_Case_0_Ret_StringValue()");
            Log.WriteLine(" Strings");
            Test_Switch_String_Case_0();
            Log.WriteLine(" ");

            #endregion

            #region Argument calls

            // Variables used as arguments to test methods
            {
                Int32 sign32 = 6;
                Int64 sign64 = 1441151880758558720;
                UInt32 unsign32 = 100;
                UInt64 unsign64 = 10223372036854775807;
                FlingOops.String str = "I am a string";

                Log.WriteLine("---Argument:");
                Test_Arg_Int32(sign32);
                Test_Arg_Int64(sign64);
                Test_Arg_UInt32(unsign32);
                Test_Arg_UInt64(unsign64);
                Test_Arg_String(str);
                Log.WriteLine(" ");
            }

            #endregion

            #region Array calls

            Log.WriteLine("---Array:");
            Log.WriteLine(" 32");
            Log.WriteLine("  Unsigned");
            Test_Array_UInt32();
            Log.WriteLine("  Signed");
            Test_Array_Int32();
            Log.WriteLine(" 64");
            Log.WriteLine("  Unsigned");
            Test_Array_UInt64();
            Log.WriteLine("  Signed");
            Test_Array_Int64();            
            Log.WriteLine(" Strings");
            Test_Array_String();
            Log.WriteLine(" Structs");
            Test_Array_Struct();
            Log.WriteLine(" ");

            #endregion

            #region Heap calls

            Log.WriteLine("---Heap:");
            Test_Heap();
            Log.WriteLine(" ");

            #endregion

            #region Strings calls

            Log.WriteLine("---String:");
            Test_Strings();
            Log.WriteLine(" ");

            #endregion

            #region Object calls

            Log.WriteLine("---Object:");

            Log.WriteLine(" ");

            #endregion

            Log.WriteLine("Tests completed.");
        }

        #region Struct

        /// <summary>
        /// Tests: Sizeof a struct in bytes, 
        /// Inputs: AStruct, 
        /// Result: Sum of the individual elements of the struct in bytes (e.g.: byte = 1, short = 2, int = 4, long = 8)
        /// </summary>
        [NoGC]
        public static unsafe void Test_Sizeof_Struct()
        {
            int size = sizeof(AStruct);
            if (size == 15)
            {
                Log.WriteSuccess("Test_Sizeof_Struct okay.");
            }
            else
            {
                Log.WriteError("Test_Sizeof_Struct NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Elements of a new instance of a struct stored and read correctly, 
        /// Inputs: AStruct, 
        /// Result: Values declared for each element
        /// </summary>
        [NoGC]
        public static void Test_Instance_Struct()
        {
            AStruct Inst = new AStruct();
            Inst.a = 1;
            Inst.b = 2;
            Inst.c = 4;
            Inst.d = 8;
            if ((Inst.a == 1) && (Inst.b == 2) && (Inst.c == 4) && (Inst.d == 8))
            {
                Log.WriteSuccess("Test_Instance_Struct okay.");
            }
            else
            {
                Log.WriteError("Test_Instance_Struct NOT okay.");
            }
        }

        #endregion

        #region Variables and pointers

        /// <summary>
        /// Tests: Local variable declaration and pointer dereferencing, 
        /// Inputs: 0xDEADBEEF, 
        /// Result: 0xDEADBEEF
        /// </summary>
        [NoGC]
        public static unsafe void Test_Locals_And_Pointers()
        {
            uint testVal = 0xDEADBEEF;
            uint* testValPtr = &testVal;
            if ((testVal == 0xDEADBEEF) && (*testValPtr == 0xDEADBEEF))
            {
                Log.WriteSuccess("Test_Locals_And_Pointers okay.");
            }
            else
            {
                Log.WriteError("Test_Locals_And_Pointers NOT okay.");
            }
        }

        #endregion

        #region Modulus

        /// <summary>
        /// Tests: Modulus (remainder) operation using unsigned 32-bit integers, 
        /// Inputs: 9, 3, 
        /// Result: 0
        /// </summary>
        [NoGC]
        public static void Test_Mod_UInt32_9_UInt32_3()
        {
            UInt32 a = 9;
            UInt32 b = 3;
            a = a % b;
            if (a == 0)
            {
                Log.WriteSuccess("Test_Mod_UInt32_9_UInt32_3 okay.");
            }
            else
            {
                Log.WriteError("Test_Mod_UInt32_9_UInt32_3 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Modulus (remainder) operation using unsigned 32-bit integers, 
        /// Inputs: 10, 3, 
        /// Result: 1
        /// </summary>
        [NoGC]
        public static void Test_Mod_UInt32_10_UInt32_3()
        {
            UInt32 a = 10;
            UInt32 b = 3;
            a = a % b;
            if (a == 1)
            {
                Log.WriteSuccess("Test_Mod_UInt32_10_UInt32_3 okay.");
            }
            else
            {
                Log.WriteError("Test_Mod_UInt32_10_UInt32_3 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Modulus (remainder) operation using signed 32-bit integers, 
        /// Inputs: -9, 3, 
        /// Result: 0
        /// </summary>
        [NoGC]
        public static void Test_Mod_Int32_Neg9_Int32_3()
        {
            Int32 a = -9;
            Int32 b = 3;
            a = a % b;
            if (a == 0)
            {
                Log.WriteSuccess("Test_Mod_Int32_Neg9_Int32_3 okay.");
            }
            else
            {
                Log.WriteError("Test_Mod_Int32_Neg9_Int32_3 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Modulus (remainder) operation using signed 32-bit integers, 
        /// Inputs: 9, -3, 
        /// Result: 0
        /// </summary>
        [NoGC]
        public static void Test_Mod_Int32_9_Int32_Neg3()
        {
            Int32 a = 9;
            Int32 b = -3;
            a = a % b;
            if (a == 0)
            {
                Log.WriteSuccess("Test_Mod_Int32_9_Int32_Neg3 okay.");
            }
            else
            {
                Log.WriteError("Test_Mod_Int32_9_Int32_Neg3 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Modulus (remainder) operation using signed 32-bit integers, 
        /// Inputs: -9, -3, 
        /// Result: 0
        /// </summary>
        [NoGC]
        public static void Test_Mod_Int32_Neg9_Int32_Neg3()
        {
            Int32 a = -9;
            Int32 b = -3;
            a = a % b;
            if (a == 0)
            {
                Log.WriteSuccess("Test_Mod_Int32_Neg9_Int32_Neg3 okay.");
            }
            else
            {
                Log.WriteError("Test_Mod_Int32_Neg9_Int32_Neg3 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Modulus (remainder) operation using signed 32-bit integers, 
        /// Inputs: 9, 3, 
        /// Result: 0
        /// </summary>
        [NoGC]
        public static void Test_Mod_Int32_9_Int32_3()
        {
            Int32 a = 9;
            Int32 b = 3;
            a = a % b;
            if (a == 0)
            {
                Log.WriteSuccess("Test_Mod_Int32_9_Int32_3 okay.");
            }
            else
            {
                Log.WriteError("Test_Mod_Int32_9_Int32_3 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Modulus (remainder) operation using signed 32-bit integers, 
        /// Inputs: -10, 3, 
        /// Result: -1
        /// </summary>
        [NoGC]
        public static void Test_Mod_Int32_Neg10_Int32_3()
        {
            Int32 a = -10;
            Int32 b = 3;
            a = a % b;
            if (a == -1)
            {
                Log.WriteSuccess("Test_Mod_Int32_Neg10_Int32_3 okay.");
            }
            else
            {
                Log.WriteError("Test_Mod_Int32_Neg10_Int32_3 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Modulus (remainder) operation using signed 32-bit integers, 
        /// Inputs: 10, -3, 
        /// Result: 1
        /// </summary>
        [NoGC]
        public static void Test_Mod_Int32_10_Int32_Neg3()
        {
            Int32 a = 10;
            Int32 b = -3;
            a = a % b;
            if (a == 1)
            {
                Log.WriteSuccess("Test_Mod_Int32_10_Int32_Neg3 okay.");
            }
            else
            {
                Log.WriteError("Test_Mod_Int32_10_Int32_Neg3 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Modulus (remainder) operation using signed 32-bit integers, 
        /// Inputs: -10, -3, 
        /// Result: -1
        /// </summary>
        [NoGC]
        public static void Test_Mod_Int32_Neg10_Int32_Neg3()
        {
            Int32 a = -10;
            Int32 b = -3;
            a = a % b;
            if (a == -1)
            {
                Log.WriteSuccess("Test_Mod_Int32_Neg10_Int32_Neg3 okay.");
            }
            else
            {
                Log.WriteError("Test_Mod_Int32_Neg10_Int32_Neg3 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Modulus (remainder) operation using signed 32-bit integers, 
        /// Inputs: 10, 3, 
        /// Result: 1
        /// </summary>
        [NoGC]
        public static void Test_Mod_Int32_10_Int32_3()
        {
            Int32 a = 10;
            Int32 b = 3;
            a = a % b;
            if (a == 1)
            {
                Log.WriteSuccess("Test_Mod_Int32_10_Int32_3 okay.");
            }
            else
            {
                Log.WriteError("Test_Mod_Int32_10_Int32_3 NOT okay.");
            }
        }

        #endregion

        #region Division

        /// <summary>
        /// Tests: Division operation using unsigned 32-bit integers, 
        /// Inputs: 9, 3, 
        /// Result: 3
        /// </summary>
        [NoGC]
        public static void Test_Div_UInt32_9_UInt32_3()
        {
            UInt32 a = 9;
            UInt32 b = 3;
            a = a / b;
            if (a == 3)
            {
                Log.WriteSuccess("Test_Div_UInt32_9_UInt32_3 okay.");
            }
            else
            {
                Log.WriteError("Test_Div_UInt32_9_UInt32_3 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Division operation using unsigned 32-bit integers, 
        /// Inputs: 10, 3, 
        /// Result: 3
        /// </summary>
        [NoGC]
        public static void Test_Div_UInt32_10_UInt32_3()
        {
            UInt32 a = 10;
            UInt32 b = 3;
            a = a / b;
            if (a == 3)
            {
                Log.WriteSuccess("Test_Div_UInt32_10_UInt32_3 okay.");
            }
            else
            {
                Log.WriteError("Test_Div_UInt32_10_UInt32_3 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Division operation using signed 32-bit integers, 
        /// Inputs: -9, 3, 
        /// Result: -3
        /// </summary>
        [NoGC]
        public static void Test_Div_Int32_Neg9_Int32_3()
        {
            Int32 a = -9;
            Int32 b = 3;
            a = a / b;
            if (a == -3)
            {
                Log.WriteSuccess("Test_Div_Int32_Neg9_Int32_3 okay.");
            }
            else
            {
                Log.WriteError("Test_Div_Int32_Neg9_Int32_3 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Division operation using signed 32-bit integers, 
        /// Inputs: 9, -3, 
        /// Result: -3
        /// </summary>
        [NoGC]
        public static void Test_Div_Int32_9_Int32_Neg3()
        {
            Int32 a = 9;
            Int32 b = -3;
            a = a / b;
            if (a == -3)
            {
                Log.WriteSuccess("Test_Div_Int32_9_Int32_Neg3 okay.");
            }
            else
            {
                Log.WriteError("Test_Div_Int32_9_Int32_Neg3 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Division operation using signed 32-bit integers, 
        /// Inputs: -9, -3, 
        /// Result: 3
        /// </summary>
        [NoGC]
        public static void Test_Div_Int32_Neg9_Int32_Neg3()
        {
            Int32 a = -9;
            Int32 b = -3;
            a = a / b;
            if (a == 3)
            {
                Log.WriteSuccess("Test_Div_Int32_Neg9_Int32_Neg3 okay.");
            }
            else
            {
                Log.WriteError("Test_Div_Int32_Neg9_Int32_Neg3 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Division operation using signed 32-bit integers, 
        /// Inputs: 9, 3, 
        /// Result: 3
        /// </summary>
        [NoGC]
        public static void Test_Div_Int32_9_Int32_3()
        {
            Int32 a = 9;
            Int32 b = 3;
            a = a / b;
            if (a == 3)
            {
                Log.WriteSuccess("Test_Div_Int32_9_Int32_3 okay.");
            }
            else
            {
                Log.WriteError("Test_Div_Int32_9_Int32_3 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Division operation using signed 32-bit integers, 
        /// Inputs: -10, 3, 
        /// Result: -3
        /// </summary>
        [NoGC]
        public static void Test_Div_Int32_Neg10_Int32_3()
        {
            Int32 a = -10;
            Int32 b = 3;
            a = a / b;
            if (a == -3)
            {
                Log.WriteSuccess("Test_Div_Int32_Neg10_Int32_3 okay.");
            }
            else
            {
                Log.WriteError("Test_Div_Int32_Neg10_Int32_3 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Division operation using signed 32-bit integers, 
        /// Inputs: 10, -3, 
        /// Result: -3
        /// </summary>
        [NoGC]
        public static void Test_Div_Int32_10_Int32_Neg3()
        {
            Int32 a = 10;
            Int32 b = -3;
            a = a / b;
            if (a == -3)
            {
                Log.WriteSuccess("Test_Div_Int32_10_Int32_Neg3 okay.");
            }
            else
            {
                Log.WriteError("Test_Div_Int32_10_Int32_Neg3 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Division operation using signed 32-bit integers, 
        /// Inputs: -10, -3, 
        /// Result: 3
        /// </summary>
        [NoGC]
        public static void Test_Div_Int32_Neg10_Int32_Neg3()
        {
            Int32 a = -10;
            Int32 b = -3;
            a = a / b;
            if (a == 3)
            {
                Log.WriteSuccess("Test_Div_Int32_Neg10_Int32_Neg3 okay.");
            }
            else
            {
                Log.WriteError("Test_Div_Int32_Neg10_Int32_Neg3 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Division operation using signed 32-bit integers, 
        /// Inputs: 10, 3, 
        /// Result: 3
        /// </summary>
        [NoGC]
        public static void Test_Div_Int32_10_Int32_3()
        {
            Int32 a = 10;
            Int32 b = 3;
            a = a / b;
            if (a == 3)
            {
                Log.WriteSuccess("Test_Div_Int32_10_Int32_3 okay.");
            }
            else
            {
                Log.WriteError("Test_Div_Int32_10_Int32_3 NOT okay.");
            }
        }

        #endregion

        #region Subtraction

        /// <summary>
        /// Tests: Subtraction operation using unsigned 32-bit integers, 
        /// Inputs: 9, 4, 
        /// Result: 5
        /// </summary>
        [NoGC]
        public static void Test_Sub_UInt32_9_UInt32_4()
        {
            UInt32 a = 9;
            UInt32 b = 4;
            a = a - b;
            if (a == 5)
            {
                Log.WriteSuccess("Test_Sub_UInt32_9_UInt32_4 okay.");
            }
            else
            {
                Log.WriteError("Test_Sub_UInt32_9_UInt32_4 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Subtraction operation using signed 32-bit integers, 
        /// Inputs: -9, 4, 
        /// Result: -13
        /// </summary>
        [NoGC]
        public static void Test_Sub_Int32_Neg9_Int32_4()
        {
            Int32 a = -9;
            Int32 b = 4;
            a = a - b;
            if (a == -13)
            {
                Log.WriteSuccess("Test_Sub_Int32_Neg9_Int32_4 okay.");
            }
            else
            {
                Log.WriteError("Test_Sub_Int32_Neg9_Int32_4 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Subtraction operation using signed 32-bit integers, 
        /// Inputs: -9, -4, 
        /// Result: -5
        /// </summary>
        [NoGC]
        public static void Test_Sub_Int32_Neg9_Int32_Neg4()
        {
            Int32 a = -9;
            Int32 b = -4;
            a = a - b;
            if (a == -5)
            {
                Log.WriteSuccess("Test_Sub_Int32_Neg9_Int32_Neg4 okay.");
            }
            else
            {
                Log.WriteError("Test_Sub_Int32_Neg9_Int32_Neg4 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Subtraction operation using signed 32-bit integers, 
        /// Inputs: 9, -4, 
        /// Result: 13
        /// </summary>
        [NoGC]
        public static void Test_Sub_Int32_9_Int32_Neg4()
        {
            Int32 a = 9;
            Int32 b = -4;
            a = a - b;
            if (a == 13)
            {
                Log.WriteSuccess("Test_Sub_Int32_9_Int32_Neg4 okay.");
            }
            else
            {
                Log.WriteError("Test_Sub_Int32_9_Int32_Neg4 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Subtraction operation using signed 32-bit integers, 
        /// Inputs: 9, 4, 
        /// Result: 5
        /// </summary>
        [NoGC]
        public static void Test_Sub_Int32_9_Int32_4()
        {
            Int32 a = 9;
            Int32 b = 4;
            a = a - b;
            if (a == 5)
            {
                Log.WriteSuccess("Test_Sub_Int32_9_Int32_4 okay.");
            }
            else
            {
                Log.WriteError("Test_Sub_Int32_9_Int32_4 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Subtraction operation using unsigned 64- and 32-bit integers, 
        /// Inputs: Largest +ve, 4, 
        /// Result: (Largest +ve - 4)
        /// </summary>
        [NoGC]
        public static void Test_Sub_UInt64_LargestPos_UInt32_4()
        {
            UInt64 a = 18446744073709551615;
            UInt32 b = 4;
            a = a - b;
            if (a == 18446744073709551611)
            {
                Log.WriteSuccess("Test_Sub_UInt64_LargestPos_UInt32_4 okay.");
            }
            else
            {
                Log.WriteError("Test_Sub_UInt64_LargestPos_UInt32_4 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Subtraction operation using signed 64- and 32-bit integers, 
        /// Inputs: Largest +ve, 4, 
        /// Result: (Largest +ve - 4)
        /// </summary>
        /// <remarks>
        /// <para> 
        /// Here a 32-bit signed integer is subtracted from a 64-bit signed integer producing a 64-bit signed value. 
        /// A largest +ve value is used for the first operand, result is (Largest +ve - 4). 
        /// While testing subtraction using 64-bit integers, it is important to handle the "borrow-bit" correctly. 
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Sub_Int64_LargestPos_Int32_4()
        {
            Int64 a = 9223372036854775807;
            Int32 b = 4;
            a = a - b;
            if (a == 9223372036854775803)
            {
                Log.WriteSuccess("Test_Sub_Int64_LargestPos_Int32_4 okay.");
            }
            else
            {
                Log.WriteError("Test_Sub_Int64_LargestPos_Int32_4 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Subtraction operation using signed 64- and 32-bit integers, 
        /// Inputs: Largest -ve, 4, 
        /// Result: (Largest +ve - 3)
        /// </summary>
        /// <remarks>
        /// <para> 
        /// Here a 32-bit signed integer is subtracted from a 64-bit signed integer producing a 64-bit signed value. 
        /// The largest -ve value is used for the first operand. Correct result should be (Largest +ve - 3) because of the 
        /// circular nature of signed numbers in two's complement.
        /// While testing subtraction using 64-bit integers, it is important to handle the "borrow-bit" correctly. 
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Sub_Int64_LargestNeg_Int32_4()
        {
            Int64 a = -9223372036854775808;
            Int32 b = 4;
            a = a - b;
            if (a == 9223372036854775804)
            {
                Log.WriteSuccess("Test_Sub_Int64_LargestNeg_Int32_4 okay.");
            }
            else
            {
                Log.WriteError("Test_Sub_Int64_LargestNeg_Int32_4 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Subtraction operation using signed 64- and 32-bit integers, 
        /// Inputs: 0, 4, 
        /// Result: -4
        /// </summary>
        /// <remarks>
        /// <para> 
        /// Here a 32-bit signed integer is subtracted from a 64-bit signed integer producing a 64-bit signed value. 
        /// Zero is used for the first operand to ensure that the 64-bit negative result is produced correctly. 
        /// While testing subtraction using 64-bit integers, it is important to handle the "borrow-bit" correctly. 
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Sub_Int64_Zero_Int32_4()
        {
            Int64 a = 0;
            Int32 b = 4;
            a = a - b;
            if (a == -4)
            {
                Log.WriteSuccess("Test_Sub_Int64_Zero_Int32_4 okay.");
            }
            else
            {
                Log.WriteError("Test_Sub_Int64_Zero_Int32_4 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Subtraction operation using signed 64- and 32-bit integers, 
        /// Inputs: 0, Largest +ve, 
        /// Result: Large -ve
        /// </summary>
        /// <remarks>
        /// <para> 
        /// Here a 32-bit signed integer is subtracted from a 64-bit signed integer producing a 64-bit signed value. 
        /// Zero is used for the first operand and the 64-bit result must be equal to -(op2). 
        /// While testing subtraction using 64-bit integers, it is important to handle the "borrow-bit" correctly. 
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Sub_Int64_Zero_Int32_LargestPos()
        {
            Int64 a = 0;
            Int32 b = 2147483647;
            a = a - b;
            if (a == -2147483647)
            {
                Log.WriteSuccess("Test_Sub_Int64_Zero_Int32_LargestPos okay.");
            }
            else
            {
                Log.WriteError("Test_Sub_Int64_Zero_Int32_LargestPos NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Subtraction operation using signed 64- and 32-bit integers, 
        /// Inputs: 0, (Largest -ve - 1), 
        /// Result: Largest +ve
        /// </summary>
        /// <remarks>
        /// <para> 
        /// Here a 32-bit signed integer is subtracted from a 64-bit signed integer producing a 64-bit signed value. 
        /// Zero is used for the first operand and the 64-bit result must be equal to -(op2). 
        /// While testing subtraction using 64-bit integers, it is important to handle the "borrow-bit" correctly. 
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Sub_Int64_Zero_Int32_LargestNeg()
        {
            Int64 a = 0;
            Int32 b = -2147483647;
            a = a - b;
            if (a == 2147483647)
            {
                Log.WriteSuccess("Test_Sub_Int64_Zero_Int32_LargestNeg okay.");
            }
            else
            {
                Log.WriteError("Test_Sub_Int64_Zero_Int32_LargestNeg NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Subtraction operation using signed 64-bit integers, 
        /// Inputs: Large +ve, 4, 
        /// Result: Large +ve
        /// </summary>
        /// <remarks>
        /// <para> 
        /// Here a 64-bit signed integer is subtracted from a 64-bit signed integer producing a 64-bit signed value. 
        /// A large value is used for the first operand to ensure that the 64-bit result is produced correctly, result is large +ve. 
        /// While testing subtraction using 64-bit integers, it is important to handle the "borrow-bit" correctly. 
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Sub_Int64_LargePos_Int64_4()
        {
            Int64 a = 1080863910568919040;
            Int64 b = 4;
            a = a - b;
            if (a == 1080863910568919036)
            {
                Log.WriteSuccess("Test_Sub_Int64_LargePos_Int64_4 okay.");
            }
            else
            {
                Log.WriteError("Test_Sub_Int64_LargePos_Int64_4 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Subtraction operation using signed 64-bit integers, 
        /// Inputs: 0, 4, 
        /// Result: -4
        /// </summary>
        /// <remarks>
        /// <para> 
        /// Here a 64-bit signed integer is subtracted from a 64-bit signed integer producing a 64-bit signed value. 
        /// Zero is used for the first operand to ensure that the 64-bit negative result is produced correctly. 
        /// While testing subtraction using 64-bit integers, it is important to handle the "borrow-bit" correctly. 
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Sub_Int64_Zero_Int64_4()
        {
            Int64 a = 0;
            Int64 b = 4;
            a = a - b;
            if (a == -4)
            {
                Log.WriteSuccess("Test_Sub_Int64_Zero_Int64_4 okay.");
            }
            else
            {
                Log.WriteError("Test_Sub_Int64_Zero_Int64_4 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Subtraction operation using unsigned 64-bit integers, 
        /// Inputs: Large +ve, Large +ve, 
        /// Result: +ve
        /// </summary>
        /// <remarks>
        /// <para> 
        /// Here a 64-bit unsigned integer is subtracted from a 64-bit unsigned integer producing a 64-bit unsigned value. 
        /// Both operands are large values but op1 > op2, therefore result must be +ve. 
        /// While testing subtraction using 64-bit integers, it is important to handle the "borrow-bit" correctly. 
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Sub_UInt64_Large_UInt64_Large()
        {
            UInt64 a = 1080863910568919040;
            UInt64 b = 844424930131968;
            a = a - b;
            if (a == 1080019485638787072)
            {
                Log.WriteSuccess("Test_Sub_UInt64_Large_UInt64_Large okay.");
            }
            else
            {
                Log.WriteError("Test_Sub_UInt64_Large_UInt64_Large NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Subtraction operation using signed 64-bit integers, 
        /// Inputs: Largest -ve, 1, 
        /// Result: Largest +ve
        /// </summary>
        /// <remarks>
        /// <para> 
        /// Here a 64-bit signed integer is subtracted from a 64-bit signed integer producing a 64-bit signed value. 
        /// The first operand is the largest -ve value, while op2 = 1. The result should be the largest +ve.
        /// While testing subtraction using 64-bit integers, it is important to handle the "borrow-bit" correctly. 
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Sub_Int64_LargestNeg_Int64_1()
        {
            Int64 a = -9223372036854775808;
            Int64 b = 1;
            a = a - b;
            if (a == 9223372036854775807)
            {
                Log.WriteSuccess("Test_Sub_Int64_LargestNeg_Int64_1 okay.");
            }
            else
            {
                Log.WriteError("Test_Sub_Int64_LargestNeg_Int64_1 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Subtraction operation using signed 64-bit integers, 
        /// Inputs: 0, Large +ve, 
        /// Result: Large -ve
        /// </summary>
        /// <remarks>
        /// <para> 
        /// Here a 64-bit signed integer is subtracted from a 64-bit signed integer producing a 64-bit signed value. 
        /// Zero is used for the first operand and the 64-bit negative result must be large. 
        /// While testing subtraction using 64-bit integers, it is important to handle the "borrow-bit" correctly. 
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Sub_Int64_Zero_Int64_LargePos()
        {
            Int64 a = 0;
            Int64 b = 844424930131968;
            a = a - b;
            if (a == -844424930131968)
            {
                Log.WriteSuccess("Test_Sub_Int64_Zero_Int64_LargePos okay.");
            }
            else
            {
                Log.WriteError("Test_Sub_Int64_Zero_Int64_LargePos NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Subtraction operation using signed 64-bit integers, 
        /// Inputs: 0, Large -ve, 
        /// Result: Large +ve
        /// </summary>
        /// <remarks>
        /// <para> 
        /// Here a 64-bit signed integer is subtracted from a 64-bit signed integer producing a 64-bit signed value. 
        /// Zero is used for the first operand and the 64-bit result must be a large +ve. 
        /// While testing subtraction using 64-bit integers, it is important to handle the "borrow-bit" correctly. 
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Sub_Int64_Zero_Int64_LargeNeg()
        {
            Int64 a = 0;
            Int64 b = -844424930131968;
            a = a - b;
            if (a == 844424930131968)
            {
                Log.WriteSuccess("Test_Sub_Int64_Zero_Int64_LargeNeg okay.");
            }
            else
            {
                Log.WriteError("Test_Sub_Int64_Zero_Int64_LargeNeg NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Subtraction operation using signed 64-bit integers, 
        /// Inputs: Large -ve, Large -ve, 
        /// Result: Large -ve
        /// </summary>
        /// <remarks>
        /// <para> 
        /// Here a 64-bit signed integer is subtracted from a 64-bit signed integer producing a 64-bit signed value. 
        /// A large -ve value is used for both operands, result must be a large -ve. 
        /// While testing subtraction using 64-bit integers, it is important to handle the "borrow-bit" correctly. 
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Sub_Int64_LargeNeg_Int64_LargeNeg()
        {
            Int64 a = -1080863910568919040;
            Int64 b = -844424930131968;
            a = a - b;
            if (a == -1080019485638787072)
            {
                Log.WriteSuccess("Test_Sub_Int64_LargeNeg_Int64_LargeNeg okay.");
            }
            else
            {
                Log.WriteError("Test_Sub_Int64_LargeNeg_Int64_LargeNeg NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Subtraction operation using signed 64-bit integers, 
        /// Inputs: Large +ve, Large -ve, 
        /// Result: Large +ve
        /// </summary>
        /// <remarks>
        /// <para> 
        /// Here a 64-bit signed integer is subtracted from a 64-bit signed integer producing a 64-bit signed value. 
        /// A large +ve value is used for op1 and a large -ve for op2, here the result is a large +ve. 
        /// While testing subtraction using 64-bit integers, it is important to handle the "borrow-bit" correctly. 
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Sub_Int64_LargePos_Int64_LargeNeg()
        {
            Int64 a = 1080863910568919040;
            Int64 b = -844424930131968;
            a = a - b;
            if (a == 1081708335499051008)
            {
                Log.WriteSuccess("Test_Sub_Int64_LargePos_Int64_LargeNeg okay.");
            }
            else
            {
                Log.WriteError("Test_Sub_Int64_LargePos_Int64_LargeNeg NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Subtraction operation using signed 64-bit integers, 
        /// Inputs: Large +ve, Large +ve, 
        /// Result: Large +ve
        /// </summary>
        /// <remarks>
        /// <para> 
        /// Here a 64-bit signed integer is subtracted from a 64-bit signed integer producing a 64-bit signed value. 
        /// A large +ve value is used for both operands, here the result is a large +ve. 
        /// While testing subtraction using 64-bit integers, it is important to handle the "borrow-bit" correctly. 
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Sub_Int64_LargePos_Int64_LargePos()
        {
            Int64 a = 1080863910568919040;
            Int64 b = 844424930131968;
            a = a - b;
            if (a == 1080019485638787072)
            {
                Log.WriteSuccess("Test_Sub_Int64_LargePos_Int64_LargePos okay.");
            }
            else
            {
                Log.WriteError("Test_Sub_Int64_LargePos_Int64_LargePos NOT okay.");
            }
        }

        #endregion

        #region Right shift

        /// <summary>
        /// Tests: Right shift operation shifting an unsigned 64-bit value, 
        /// Inputs: 64-bit, 10, 
        /// Result: 64-bit.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_RShift_UInt64_Large_Int32_10()
        {
            UInt64 a = 576460752303423488;
            Int32 b = 10;
            a = a >> b;
            if (a == 562949953421312)
            {
                Log.WriteSuccess("Test_RShift_UInt64_Large_Int32_10 okay.");
            }
            else
            {
                Log.WriteError("Test_RShift_UInt64_Large_Int32_10 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Right shift operation shifting a signed 32-bit value, 
        /// Inputs: 32-bit -ve, 6, 
        /// Result: 32-bit -ve (padded with 1s).
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_RShift_Int32_SmallNeg_Int32_6()
        {
            Int32 a = -28416;
            Int32 b = 6;
            a = a >> b;
            if (a == -444)
            {
                Log.WriteSuccess("Test_RShift_Int32_SmallNeg_Int32_6 okay.");
            }
            else
            {
                Log.WriteError("Test_RShift_Int32_SmallNeg_Int32_6 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Right shift operation shifting an unsigned 32-bit value, 
        /// Inputs: 32-bit, 6, 
        /// Result: 32-bit.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_RShift_UInt32_Small_Int32_6()
        {
            UInt32 a = 4352;
            Int32 b = 6;
            a = a >> b;
            if (a == 68)
            {
                Log.WriteSuccess("Test_RShift_UInt32_Small_Int32_6 okay.");
            }
            else
            {
                Log.WriteError("Test_RShift_UInt32_Small_Int32_6 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Right shift operation shifting a signed 64-bit value, 
        /// Inputs: 64-bit -ve, 6, 
        /// Result: 64-bit -ve (stored as 64-bit padded with 1s).
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_RShift_Int64_LargeNeg_Int32_6()
        {
            Int64 a = -9185091440022126524;
            Int32 b = 6;
            a = a >> b;
            if (a == -143517053750345727)
            {
                Log.WriteSuccess("Test_RShift_Int64_LargeNeg_Int32_6 okay.");
            }
            else
            {
                Log.WriteError("Test_RShift_Int64_LargeNeg_Int32_6 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Right shift operation shifting a signed 64-bit value, 
        /// Inputs: 64-bit -ve, 40, 
        /// Result: 32-bit -ve (stored as 64-bit padded with 1s).
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_RShift_Int64_LargeNeg_Int32_40()
        {
            Int64 a = -9187343239835811840;
            Int32 b = 40;
            a = a >> b;
            if (a == -8355840)
            {
                Log.WriteSuccess("Test_RShift_Int64_LargeNeg_Int32_40 okay.");
            }
            else
            {
                Log.WriteError("Test_RShift_Int64_LargeNeg_Int32_40 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Right shift operation shifting a signed 64-bit value, 
        /// Inputs: Largest 64-bit +ve, 40, 
        /// Result: 32-bit +ve (stored as 64-bit padded with 0s).
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_RShift_Int64_LargestPos_Int32_40()
        {
            Int64 a = 9223372036854775807;
            Int32 b = 40;
            a = a >> b;
            if (a == 8388607)
            {
                Log.WriteSuccess("Test_RShift_Int64_LargestPos_Int32_40 okay.");
            }
            else
            {
                Log.WriteError("Test_RShift_Int64_LargestPos_Int32_40 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Right shift operation shifting a signed 64-bit value, 
        /// Inputs: Largest 64-bit -ve, 40, 
        /// Result: 32-bit -ve (stored as 64-bit padded with 1s).
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_RShift_Int64_LargestNeg_Int32_40()
        {
            Int64 a = -9223372036854775808;
            Int32 b = 40;
            a = a >> b;
            if (a == -8388608)
            {
                Log.WriteSuccess("Test_RShift_Int64_LargestNeg_Int32_40 okay.");
            }
            else
            {
                Log.WriteError("Test_RShift_Int64_LargestNeg_Int32_40 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Right shift operation shifting a signed 64-bit value, 
        /// Inputs: -1, 63, 
        /// Result: -1 because of circular nature of two's complement.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_RShift_Int64_Neg1_Int32_63()
        {
            Int64 a = -1;
            Int32 b = 63;
            a = a >> b;
            if (a == -1)
            {
                Log.WriteSuccess("Test_RShift_Int64_Neg1_Int32_63 okay.");
            }
            else
            {
                Log.WriteError("Test_RShift_Int64_Neg1_Int32_63 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Right shift operation shifting an unsigned 64-bit value, 
        /// Inputs: Largest, 63, 
        /// Result: 1.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_RShift_UInt64_Largest_Int32_63()
        {
            UInt64 a = 18446744073709551615;
            Int32 b = 63;
            a = a >> b;
            if (a == 1)
            {
                Log.WriteSuccess("Test_RShift_UInt64_Largest_Int32_63 okay.");
            }
            else
            {
                Log.WriteError("Test_RShift_UInt64_Largest_Int32_63 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Right shift operation shifting an unsigned 32-bit value, 
        /// Inputs: Largest, 6, 
        /// Result: 32-bit.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_RShift_UInt32_Largest_Int32_6()
        {
            UInt32 a = 4294967295;
            Int32 b = 6;
            a = a >> b;
            if (a == 67108863)
            {
                Log.WriteSuccess("Test_RShift_UInt32_Largest_Int32_6 okay.");
            }
            else
            {
                Log.WriteError("Test_RShift_UInt32_Largest_Int32_6 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Right shift operation shifting a signed 32-bit value, 
        /// Inputs: Small +ve, 6, 
        /// Result: 32-bit.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_RShift_Int32_SmallPos_Int32_6()
        {
            Int32 a = 255;
            Int32 b = 6;
            a = a >> b;
            if (a == 3)
            {
                Log.WriteSuccess("Test_RShift_Int32_SmallPos_Int32_6 okay.");
            }
            else
            {
                Log.WriteError("Test_RShift_Int32_SmallPos_Int32_6 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Right shift operation shifting a signed 32-bit value, 
        /// Inputs: Largest +ve, 6, 
        /// Result: 32-bit.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_RShift_Int32_LargestPos_Int32_6()
        {
            Int32 a = 2147483647;
            Int32 b = 6;
            a = a >> b;
            if (a == 33554431)
            {
                Log.WriteSuccess("Test_RShift_Int32_LargestPos_Int32_6 okay.");
            }
            else
            {
                Log.WriteError("Test_RShift_Int32_LargestPos_Int32_6 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Right shift operation shifting a signed 32-bit value, 
        /// Inputs: Largest -ve, 6, 
        /// Result: 32-bit.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_RShift_Int32_LargestNeg_Int32_6()
        {
            Int32 a = -2147483648;
            Int32 b = 6;
            a = a >> b;
            if (a == -33554432)
            {
                Log.WriteSuccess("Test_RShift_Int32_LargestNeg_Int32_6 okay.");
            }
            else
            {
                Log.WriteError("Test_RShift_Int32_LargestNeg_Int32_6 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Right shift operation shifting an unsigned 64-bit value, 
        /// Inputs: Largest, 10, 
        /// Result: 64-bit.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_RShift_UInt64_Largest_Int32_10()
        {
            UInt64 a = 18446744073709551615;
            Int32 b = 10;
            a = a >> b;
            if (a == 18014398509481983)
            {
                Log.WriteSuccess("Test_RShift_UInt64_Largest_Int32_10 okay.");
            }
            else
            {
                Log.WriteError("Test_RShift_UInt64_Largest_Int32_10 NOT okay.");
            }
        }

        #endregion

        #region Left shift

        /// <summary>
        /// Tests: Left shift operation shifting an unsigned 64-bit value, 
        /// Inputs: 64-bit, 2, 
        /// Result: 64-bit.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_LShift_UInt64_Large_Int32_2()
        {
            UInt64 a = 576460752303423488;
            Int32 b = 2;
            a = a << b;
            if (a == 2305843009213693952)
            {
                Log.WriteSuccess("Test_LShift_UInt64_Large_Int32_2 okay.");
            }
            else
            {
                Log.WriteError("Test_LShift_UInt64_Large_Int32_2 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Left shift operation shifting a signed 32-bit value, 
        /// Inputs: 32-bit -ve, 6, 
        /// Result: 32-bit -ve.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_LShift_Int32_SmallNeg_Int32_6()
        {
            Int32 a = -28416;
            Int32 b = 6;
            a = a << b;
            if (a == -1818624)
            {
                Log.WriteSuccess("Test_LShift_Int32_SmallNeg_Int32_6 okay.");
            }
            else
            {
                Log.WriteError("Test_LShift_Int32_SmallNeg_Int32_6 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Right shift operation shifting an unsigned 32-bit value, 
        /// Inputs: 32-bit, 6, 
        /// Result: 32-bit.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_LShift_UInt32_Small_Int32_6()
        {
            UInt32 a = 4352;
            Int32 b = 6;
            a = a << b;
            if (a == 278528)
            {
                Log.WriteSuccess("Test_LShift_UInt32_Small_Int32_6 okay.");
            }
            else
            {
                Log.WriteError("Test_LShift_UInt32_Small_Int32_6 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Left shift operation shifting a signed 64-bit value, 
        /// Inputs: 64-bit -ve, 6, 
        /// Result: 64-bit +ve.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_LShift_Int64_LargeNeg_Int32_6()
        {
            Int64 a = -9185091440022126524;
            Int32 b = 6;
            a = a << b;
            if (a == 2449958197289554176)
            {
                Log.WriteSuccess("Test_LShift_Int64_LargeNeg_Int32_6 okay.");
            }
            else
            {
                Log.WriteError("Test_LShift_Int64_LargeNeg_Int32_6 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Left shift operation shifting a signed 64-bit value, 
        /// Inputs: 64-bit -ve, 40, 
        /// Result: 32-bit +ve.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_LShift_Int64_LargeNeg_Int32_40()
        {
            Int64 a = -9187343239835811832;
            Int32 b = 40;
            a = a << b;
            if (a == 8796093022208)
            {
                Log.WriteSuccess("Test_LShift_Int64_LargeNeg_Int32_40 okay.");
            }
            else
            {
                Log.WriteError("Test_LShift_Int64_LargeNeg_Int32_40 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Left shift operation shifting a signed 64-bit value, 
        /// Inputs: Largest 64-bit +ve, 40, 
        /// Result: 64-bit -ve.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_LShift_Int64_LargestPos_Int32_40()
        {
            Int64 a = 9223372036854775807;
            Int32 b = 40;
            a = a << b;
            if (a == -1099511627776)
            {
                Log.WriteSuccess("Test_LShift_Int64_LargestPos_Int32_40 okay.");
            }
            else
            {
                Log.WriteError("Test_LShift_Int64_LargestPos_Int32_40 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Left shift operation shifting a signed 64-bit value, 
        /// Inputs: Largest -ve, 40, 
        /// Result: 0.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_LShift_Int64_LargestNeg_Int32_40()
        {
            Int64 a = -9223372036854775808;
            Int32 b = 40;
            a = a << b;
            if (a == 0)
            {
                Log.WriteSuccess("Test_LShift_Int64_LargestNeg_Int32_40 okay.");
            }
            else
            {
                Log.WriteError("Test_LShift_Int64_LargestNeg_Int32_40 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Left shift operation shifting a signed 64-bit value, 
        /// Inputs: -1, 63, 
        /// Result: Largest -ve.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_LShift_Int64_Neg1_Int32_63()
        {
            Int64 a = -1;
            Int32 b = 63;
            a = a << b;
            if (a == -9223372036854775808)
            {
                Log.WriteSuccess("Test_LShift_Int64_Neg1_Int32_63 okay.");
            }
            else
            {
                Log.WriteError("Test_LShift_Int64_Neg1_Int32_63 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Left shift operation shifting an unsigned 64-bit value, 
        /// Inputs: Largest, 63, 
        /// Result: 0x8000000000000000 (Highest bit set to 1).
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_LShift_UInt64_Largest_Int32_63()
        {
            UInt64 a = 18446744073709551615;
            Int32 b = 63;
            a = a << b;
            if (a == 0x8000000000000000)
            {
                Log.WriteSuccess("Test_LShift_UInt64_Largest_Int32_63 okay.");
            }
            else
            {
                Log.WriteError("Test_LShift_UInt64_Largest_Int32_63 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Left shift operation shifting an unsigned 32-bit value, 
        /// Inputs: Largest, 6, 
        /// Result: 32-bit.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_LShift_UInt32_Largest_Int32_6()
        {
            UInt32 a = 4294967295;
            Int32 b = 6;
            a = a << b;
            if (a == 4294967232)
            {
                Log.WriteSuccess("Test_LShift_UInt32_Largest_Int32_6 okay.");
            }
            else
            {
                Log.WriteError("Test_LShift_UInt32_Largest_Int32_6 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Right shift operation shifting a signed 32-bit value, 
        /// Inputs: Small +ve, 6, 
        /// Result: 32-bit.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_LShift_Int32_SmallPos_Int32_6()
        {
            Int32 a = 255;
            Int32 b = 6;
            a = a << b;
            if (a == 16320)
            {
                Log.WriteSuccess("Test_LShift_Int32_SmallPos_Int32_6 okay.");
            }
            else
            {
                Log.WriteError("Test_LShift_Int32_SmallPos_Int32_6 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Left shift operation shifting a signed 32-bit value, 
        /// Inputs: Largest +ve, 6, 
        /// Result: 32-bit -ve.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_LShift_Int32_LargestPos_Int32_6()
        {
            Int32 a = 2147483647;
            Int32 b = 6;
            a = a << b;
            if (a == -64)
            {
                Log.WriteSuccess("Test_LShift_Int32_LargestPos_Int32_6 okay.");
            }
            else
            {
                Log.WriteError("Test_LShift_Int32_LargestPos_Int32_6 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Right shift operation shifting a signed 32-bit value, 
        /// Inputs: Largest -ve, 6, 
        /// Result: 0.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_LShift_Int32_LargestNeg_Int32_6()
        {
            Int32 a = -2147483648;
            Int32 b = 1;
            a = a << b;
            if (a == 0)
            {
                Log.WriteSuccess("Test_LShift_Int32_LargestNeg_Int32_6 okay.");
            }
            else
            {
                Log.WriteError("Test_LShift_Int32_LargestNeg_Int32_6 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Right shift operation shifting an unsigned 64-bit value, 
        /// Inputs: Largest, 10, 
        /// Result: 64-bit.
        /// </summary>
        /// <remarks>
        /// <para>
        /// C# requires that the distance value is a signed 32-bit integer. 
        /// Only low order 5-bit is used when a 32-bit values is shifted, while low order 6-bit if a 64-bit value is shifted.
        /// In other words, a 32-/64-bit value cannot be pushed by more than 31/63 bits.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_LShift_UInt64_Largest_Int32_10()
        {
            UInt64 a = 18446744073709551615;
            Int32 b = 10;
            a = a << b;
            if (a == 18446744073709550592)
            {
                Log.WriteSuccess("Test_LShift_UInt64_Largest_Int32_10 okay.");
            }
            else
            {
                Log.WriteError("Test_LShift_UInt64_Largest_Int32_10 NOT okay.");
            }
        }

        #endregion

        #region Negation

        /// <summary>
        /// Tests: Negation operation using a signed 64-bit value, 
        /// Input: 64-bit (Largest -ve) - 1, 
        /// Result: 64-bit Largest +ve.
        /// </summary>
        [NoGC]
        public static void Test_Neg_Int64_LargestNeg_Int64()
        {
            Int64 a = -9223372036854775807;
            Int64 b = -a;
            if (b == 9223372036854775807)
            {
                Log.WriteSuccess("Test_Neg_Int64_LargestNeg_Int64 okay.");
            }
            else
            {
                Log.WriteError("Test_Neg_Int64_LargestNeg_Int64 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Negation operation using a signed 32-bit value, 
        /// Input: 32-bit Small -ve, 
        /// Result: 32-bit Small +ve.
        /// </summary>
        [NoGC]
        public static void Test_Neg_Int32_SmallNeg_Int32()
        {
            Int32 a = -100;
            Int32 b = -a;
            if (b == 100)
            {
                Log.WriteSuccess("Test_Neg_Int32_SmallNeg_Int32 okay.");
            }
            else
            {
                Log.WriteError("Test_Neg_Int32_SmallNeg_Int32 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Negation operation using an unsigned 32-bit value, 
        /// Input: 32-bit Largest, 
        /// Result: 64-bit -ve.
        /// </summary>
        [NoGC]
        public static void Test_Neg_UInt32_Largest_Int64()
        {
            UInt32 a = 4294967295;
            Int64 b = -a;
            if (b == -4294967295)
            {
                Log.WriteSuccess("Test_Neg_UInt32_Largest_Int64 okay.");
            }
            else
            {
                Log.WriteError("Test_Neg_UInt32_Largest_Int64 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Negation operation using a signed 64-bit value, 
        /// Input: 64-bit Largest +ve, 
        /// Result: 64-bit (Largest -ve) - 1.
        /// </summary>
        [NoGC]
        public static void Test_Neg_Int64_LargestPos_Int64()
        {
            Int64 a = 9223372036854775807;
            Int64 b = -a;
            if (b == -9223372036854775807)
            {
                Log.WriteSuccess("Test_Neg_Int64_LargestPos_Int64 okay.");
            }
            else
            {
                Log.WriteError("Test_Neg_Int64_LargestPos_Int64 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Negation operation using a signed 64-bit value, 
        /// Input: 64-bit Large +ve, 
        /// Result: 64-bit Large -ve.
        /// </summary>
        [NoGC]
        public static void Test_Neg_Int64_LargePos_Int64()
        {
            Int64 a = 372036854775807;
            Int64 b = -a;
            if (b == -372036854775807)
            {
                Log.WriteSuccess("Test_Neg_Int64_LargePos_Int64 okay.");
            }
            else
            {
                Log.WriteError("Test_Neg_Int64_LargePos_Int64 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Negation operation using a signed 32-bit value, 
        /// Input: 32-bit Small +ve, 
        /// Result: 32-bit Small -ve.
        /// </summary>
        [NoGC]
        public static void Test_Neg_Int32_SmallPos_Int32()
        {
            Int32 a = 100;
            Int32 b = -a;
            if (b == -100)
            {
                Log.WriteSuccess("Test_Neg_Int32_SmallPos_Int32 okay.");
            }
            else
            {
                Log.WriteError("Test_Neg_Int32_SmallPos_Int32 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Negation operation using a signed 64-bit value, 
        /// Input: 64-bit Large -ve, 
        /// Result: 64-bit Large +ve.
        /// </summary>
        [NoGC]
        public static void Test_Neg_Int64_LargeNeg_Int64()
        {
            Int64 a = -3372036854775807;
            Int64 b = -a;
            if (b == 3372036854775807)
            {
                Log.WriteSuccess("Test_Neg_Int64_LargeNeg_Int64 okay.");
            }
            else
            {
                Log.WriteError("Test_Neg_Int64_LargeNeg_Int64 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Negation operation using an unsigned 32-bit value, 
        /// Input: 32-bit Small, 
        /// Result: 64-bit -ve.
        /// </summary>
        [NoGC]
        public static void Test_Neg_UInt32_Small_Int64()
        {
            UInt32 a = 1;
            Int64 b = -a;
            if (b == -1)
            {
                Log.WriteSuccess("Test_Neg_UInt32_Small_Int64 okay.");
            }
            else
            {
                Log.WriteError("Test_Neg_UInt32_Small_Int64 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Negation operation using a signed 32-bit value, 
        /// Input: 32-bit Large +ve, 
        /// Result: 32-bit Large -ve as a 64-bit value.
        /// </summary>
        [NoGC]
        public static void Test_Neg_Int32_LargePos_Int64()
        {
            Int32 a = 1000000000;
            Int64 b = -a;
            if (b == -1000000000)
            {
                Log.WriteSuccess("Test_Neg_Int32_LargePos_Int64 okay.");
            }
            else
            {
                Log.WriteError("Test_Neg_Int32_LargePos_Int64 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Negation operation using a signed 32-bit value, 
        /// Input: 32-bit Large -ve, 
        /// Result: 32-bit Large +ve as a 64-bit value.
        /// </summary>
        [NoGC]
        public static void Test_Neg_Int32_LargeNeg_Int64()
        {
            Int32 a = -1000000000;
            Int64 b = -a;
            if (b == 1000000000)
            {
                Log.WriteSuccess("Test_Neg_Int32_LargeNeg_Int64 okay.");
            }
            else
            {
                Log.WriteError("Test_Neg_Int32_LargeNeg_Int64 NOT okay.");
            }
        }

        #endregion

        #region Not

        /// <summary>
        /// Tests: Not operation using a signed 64-bit value, 
        /// Input: 64-bit (Largest -ve) - 1, 
        /// Result: 64-bit (Largest +ve) - 1 because of two's complement.
        /// </summary>
        [NoGC]
        public static void Test_Not_Int64_LargestNeg_Int64()
        {
            Int64 a = -9223372036854775807;
            Int64 b = ~a;
            if (b == 9223372036854775806)
            {
                Log.WriteSuccess("Test_Not_Int64_LargestNeg_Int64 okay.");
            }
            else
            {
                Log.WriteError("Test_Not_Int64_LargestNeg_Int64 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Not operation using a signed 32-bit value, 
        /// Input: 32-bit Small -ve, 
        /// Result: 32-bit Small +ve.
        /// </summary>
        [NoGC]
        public static void Test_Not_Int32_SmallNeg_Int32()
        {
            Int32 a = -100;
            Int32 b = ~a;
            if (b == 99)
            {
                Log.WriteSuccess("Test_Not_Int32_SmallNeg_Int32 okay.");
            }
            else
            {
                Log.WriteError("Test_Not_Int32_SmallNeg_Int32 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Not operation using an unsigned 32-bit value, 
        /// Input: 32-bit Largest, 
        /// Result: 64-bit -ve.
        /// </summary>
        [NoGC]
        public static void Test_Not_UInt32_Largest_Int64()
        {
            UInt32 a = 4294967295;
            Int64 b = ~a;
            if (b == 0)
            {
                Log.WriteSuccess("Test_Not_UInt32_Largest_Int64 okay.");
            }
            else
            {
                Log.WriteError("Test_Not_UInt32_Largest_Int64 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Not operation using a signed 64-bit value, 
        /// Input: 64-bit Largest +ve, 
        /// Result: 64-bit Largest -ve.
        /// </summary>
        [NoGC]
        public static void Test_Not_Int64_LargestPos_Int64()
        {
            Int64 a = 9223372036854775807;
            Int64 b = ~a;
            if (b == -9223372036854775808)
            {
                Log.WriteSuccess("Test_Not_Int64_LargestPos_Int64 okay.");
            }
            else
            {
                Log.WriteError("Test_Not_Int64_LargestPos_Int64 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Not operation using a signed 64-bit value, 
        /// Input: 64-bit Large +ve, 
        /// Result: 64-bit Large -ve.
        /// </summary>
        [NoGC]
        public static void Test_Not_Int64_LargePos_Int64()
        {
            Int64 a = 372036854775807;
            Int64 b = ~a;
            if (b == -372036854775808)
            {
                Log.WriteSuccess("Test_Not_Int64_LargePos_Int64 okay.");
            }
            else
            {
                Log.WriteError("Test_Not_Int64_LargePos_Int64 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Not operation using a signed 32-bit value, 
        /// Input: 32-bit Small +ve, 
        /// Result: 32-bit Small -ve.
        /// </summary>
        [NoGC]
        public static void Test_Not_Int32_SmallPos_Int32()
        {
            Int32 a = 100;
            Int32 b = ~a;
            if (b == -101)
            {
                Log.WriteSuccess("Test_Not_Int32_SmallPos_Int32 okay.");
            }
            else
            {
                Log.WriteError("Test_Not_Int32_SmallPos_Int32 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Not operation using a signed 64-bit value, 
        /// Input: 64-bit Large -ve, 
        /// Result: 64-bit Large +ve.
        /// </summary>
        [NoGC]
        public static void Test_Not_Int64_LargeNeg_Int64()
        {
            Int64 a = -3372036854775807;
            Int64 b = -a;
            if (b == 3372036854775807)
            {
                Log.WriteSuccess("Test_Not_Int64_LargeNeg_Int64 okay.");
            }
            else
            {
                Log.WriteError("Test_Not_Int64_LargeNeg_Int64 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Not operation using an unsigned 32-bit value, 
        /// Input: 32-bit Small, 
        /// Result: 32-bit Small +ve as a 64-bit value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// First the not operation is applied to the 32-bit value then it is expanded to 64-bit by padding the high 32 bits with 0s.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Not_UInt32_Small_Int64()
        {
            UInt32 a = 1;
            Int64 b = ~a;
            if (b == 4294967294)
            {
                Log.WriteSuccess("Test_Not_UInt32_Small_Int64 okay.");
            }
            else
            {
                Log.WriteError("Test_Not_UInt32_Small_Int64 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Not operation using a signed 32-bit value, 
        /// Input: 32-bit Large +ve, 
        /// Result: 32-bit Large +ve as a 64-bit value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// First the not operation is applied to the 32-bit value then it is expanded to 64-bit by padding the high 32 bits with 1s.
        /// In this case it is padded with 1s because not(a)'s highest bit is set to 1, therefore C# expands the value to 64-bit according to the
        /// sign of the not-ed value. I.e.: not(+ve) is padded with 1s, while not(-ve) is padded with 0s.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Not_Int32_LargePos_Int64()
        {
            Int32 a = 1000000000;
            Int64 b = ~a;
            if (b == -1000000001)
            {
                Log.WriteSuccess("Test_Not_Int32_LargePos_Int64 okay.");
            }
            else
            {
                Log.WriteError("Test_Not_Int32_LargePos_Int64 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Not operation using a signed 32-bit value, 
        /// Input: 32-bit Large -ve, 
        /// Result: 32-bit Large +ve as a 64-bit value.
        /// </summary>
        /// /// <remarks>
        /// <para>
        /// First the not operation is applied to the 32-bit value then it is expanded to 64-bit by padding the high 32 bits with 0s.
        /// In this case it is padded with 0s because not(a)'s highest bit is set to 0, therefore C# expands the value to 64-bit according to the
        /// sign of the not-ed value. I.e.: not(+ve) is padded with 1s, while not(-ve) is padded with 0s.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Not_Int32_LargeNeg_Int64()
        {
            Int32 a = -1000000000;
            Int64 b = ~a;
            if (b == 999999999)
            {
                Log.WriteSuccess("Test_Not_Int32_LargeNeg_Int64 okay.");
            }
            else
            {
                Log.WriteError("Test_Not_Int32_LargeNeg_Int64 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Not operation using an unsigned 64-bit value, 
        /// Input: 64-bit Largest, 
        /// Result: 64-bit Smallest.
        /// </summary>
        [NoGC]
        public static void Test_Not_UInt64_Largest_UInt64()
        {
            UInt64 a = 0xFFFFFFFFFFFFFFFF;
            UInt64 b = ~a;
            if (b == 0)
            {
                Log.WriteSuccess("Test_Not_UInt64_Largest_UInt64 okay.");
            }
            else
            {
                Log.WriteError("Test_Not_UInt64_Largest_UInt64 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Not operation using an unsigned 64-bit value, 
        /// Input: 64-bit Smallest, 
        /// Result: 64-bit Largest.
        /// </summary>
        [NoGC]
        public static void Test_Not_UInt64_Smallest_UInt64()
        {
            UInt64 a = 0;
            UInt64 b = ~a;
            if (b == 18446744073709551615)
            {
                Log.WriteSuccess("Test_Not_UInt64_Smallest_UInt64 okay.");
            }
            else
            {
                Log.WriteError("Test_Not_UInt64_Smallest_UInt64 NOT okay.");
            }
        }

        #endregion

        #region Addition

        /// <summary>
        /// Tests: Addition operation using unsigned 32-bit integers, 
        /// Inputs: 0, 0, 
        /// Result: 0
        /// </summary>
        [NoGC]
        public static void Test_Add_UInt32_Zero_UInt32_Zero()
        {
            UInt32 a = 0;
            UInt32 b = 0;
            a = a + b;
            if (a == 0)
            {
                Log.WriteSuccess("Test_Add_UInt32_Zero_UInt32_Zero okay.");
            }
            else
            {
                Log.WriteError("Test_Add_UInt32_Zero_UInt32_Zero NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Addition operation using unsigned 32-bit integers, 
        /// Inputs: Small, Small, 
        /// Result: Small
        /// </summary>
        [NoGC]
        public static void Test_Add_UInt32_9_UInt32_4()
        {
            UInt32 a = 9;
            UInt32 b = 4;
            a = a + b;
            if (a == 13)
            {
                Log.WriteSuccess("Test_Add_UInt32_9_UInt32_4 okay.");
            }
            else
            {
                Log.WriteError("Test_Add_UInt32_9_UInt32_4 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Addition operation using signed 32-bit integers, 
        /// Inputs: Small -ve, Small +ve, 
        /// Result: Small -ve
        /// </summary>
        [NoGC]
        public static void Test_Add_Int32_Neg9_Int32_4()
        {
            Int32 a = -9;
            Int32 b = 4;
            a = a + b;
            if (a == -5)
            {
                Log.WriteSuccess("Test_Add_Int32_Neg9_Int32_4 okay.");
            }
            else
            {
                Log.WriteError("Test_Add_Int32_Neg9_Int32_4 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Addition operation using signed 32-bit integers, 
        /// Inputs: Small -ve, Small -ve, 
        /// Result: Small -ve
        /// </summary>
        [NoGC]
        public static void Test_Add_Int32_Neg9_Int32_Neg4()
        {
            Int32 a = -9;
            Int32 b = -4;
            a = a + b;
            if (a == -13)
            {
                Log.WriteSuccess("Test_Add_Int32_Neg9_Int32_Neg4 okay.");
            }
            else
            {
                Log.WriteError("Test_Add_Int32_Neg9_Int32_Neg4 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Addition operation using signed 32-bit integers, 
        /// Inputs: Small +ve, Small-ve, 
        /// Result: Small +ve
        /// </summary>
        [NoGC]
        public static void Test_Add_Int32_9_Int32_Neg4()
        {
            Int32 a = 9;
            Int32 b = -4;
            a = a + b;
            if (a == 5)
            {
                Log.WriteSuccess("Test_Add_Int32_9_Int32_Neg4 okay.");
            }
            else
            {
                Log.WriteError("Test_Add_Int32_9_Int32_Neg4 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Addition operation using signed 32-bit integers, 
        /// Inputs: Small +ve, Small +ve, 
        /// Result: Small +ve
        /// </summary>
        [NoGC]
        public static void Test_Add_Int32_9_Int32_4()
        {
            Int32 a = 9;
            Int32 b = 4;
            a = a + b;
            if (a == 13)
            {
                Log.WriteSuccess("Test_Add_Int32_9_Int32_4 okay.");
            }
            else
            {
                Log.WriteError("Test_Add_Int32_9_Int32_4 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Addition operation using unsigned 64- and 32-bit integers, 
        /// Inputs: Largest - 4, 4, 
        /// Result: Largest
        /// </summary>
        /// <remarks>
        /// <para>
        /// When adding 64-bit values, care must be taken to handle the carry-bit correctly
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Add_UInt64_LargestPos_UInt32_4()
        {
            UInt64 a = 18446744073709551611;
            UInt32 b = 4;
            a = a + b;
            if (a == 18446744073709551615)
            {
                Log.WriteSuccess("Test_Add_UInt64_LargestPos_UInt32_4 okay.");
            }
            else
            {
                Log.WriteError("Test_Add_UInt64_LargestPos_UInt32_4 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Addition operation using signed 64- and 32-bit integers, 
        /// Inputs: Largest +ve, Small +ve, 
        /// Result: (Largest +ve) + 4 = (Largest -ve) + 3 - circularity of two's complement 
        /// </summary>
        /// <remarks>
        /// <para>
        /// When adding 64-bit values, care must be taken to handle the carry-bit correctly
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Add_Int64_LargestPos_Int32_4()
        {
            Int64 a = 9223372036854775807;
            Int32 b = 4;
            a = a + b;
            if (a == -9223372036854775805)
            {
                Log.WriteSuccess("Test_Add_Int64_LargestPos_Int32_4 okay.");
            }
            else
            {
                Log.WriteError("Test_Add_Int64_LargestPos_Int32_4 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Addition operation using signed 64- and 32-bit integers, 
        /// Inputs: Largest -ve, Small +ve, 
        /// Result: (Largest -ve) + 4
        /// </summary>
        /// <remarks>
        /// <para>
        /// When adding 64-bit values, care must be taken to handle the carry-bit correctly
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Add_Int64_LargestNeg_Int32_4()
        {
            Int64 a = -9223372036854775808;
            Int32 b = 4;
            a = a + b;
            if (a == -9223372036854775804)
            {
                Log.WriteSuccess("Test_Add_Int64_LargestNeg_Int32_4 okay.");
            }
            else
            {
                Log.WriteError("Test_Add_Int64_LargestNeg_Int32_4 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Addition operation using signed 64- and 32-bit integers, 
        /// Inputs: 0, Largest -ve, 
        /// Result: Largest +ve
        /// </summary>
        /// <remarks>
        /// <para>
        /// When adding 64-bit values, care must be taken to handle the carry-bit correctly
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Add_Int64_Zero_Int32_LargestNeg()
        {
            Int64 a = 0;
            Int32 b = -2147483648;
            a = a + b;
            if (a == -2147483648)
            {
                Log.WriteSuccess("Test_Add_Int64_Zero_Int32_LargestNeg okay.");
            }
            else
            {
                Log.WriteError("Test_Add_Int64_Zero_Int32_LargestNeg NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Addition operation using signed 64-bit integers, 
        /// Inputs: Large +ve, 4, 
        /// Result: Large +ve
        /// </summary>
        /// <remarks>
        /// <para>
        /// When adding 64-bit values, care must be taken to handle the carry-bit correctly
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Add_Int64_LargePos_Int64_4()
        {
            Int64 a = 1080863910568919040;
            Int64 b = 4;
            a = a + b;
            if (a == 1080863910568919044)
            {
                Log.WriteSuccess("Test_Add_Int64_LargePos_Int64_4 okay.");
            }
            else
            {
                Log.WriteError("Test_Add_Int64_LargePos_Int64_4 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Addition operation using signed 64-bit integers, 
        /// Inputs: 0, -4, 
        /// Result: -4
        /// </summary>
        /// <remarks>
        /// <para>
        /// When adding 64-bit values, care must be taken to handle the carry-bit correctly
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Add_Int64_Zero_Int64_Neg4()
        {
            Int64 a = 0;
            Int64 b = -4;
            a = a + b;
            if (a == -4)
            {
                Log.WriteSuccess("Test_Add_Int64_Zero_Int64_Neg4 okay.");
            }
            else
            {
                Log.WriteError("Test_Add_Int64_Zero_Int64_Neg4 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Addition operation using unsigned 64-bit integers, 
        /// Inputs: Large +ve, Large +ve, 
        /// Result: +ve
        /// </summary>
        /// <remarks>
        /// <para>
        /// When adding 64-bit values, care must be taken to handle the carry-bit correctly
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Add_UInt64_Large_UInt64_Large()
        {
            UInt64 a = 108086391056891904;
            UInt64 b = 844424930131968;
            a = a + b;
            if (a == 108930815987023872)
            {
                Log.WriteSuccess("Test_Add_UInt64_Large_UInt64_Large okay.");
            }
            else
            {
                Log.WriteError("Test_Add_UInt64_Large_UInt64_Large NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Addition operation using signed 64-bit integers, 
        /// Inputs: Largest -ve, -1, 
        /// Result: Largest +ve
        /// </summary>
        /// <remarks>
        /// <para>
        /// When adding 64-bit values, care must be taken to handle the carry-bit correctly
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Add_Int64_LargestNeg_Int64_Neg1()
        {
            Int64 a = -9223372036854775808;
            Int64 b = -1;
            a = a + b;
            if (a == 9223372036854775807)
            {
                Log.WriteSuccess("Test_Add_Int64_LargestNeg_Int64_Neg1 okay.");
            }
            else
            {
                Log.WriteError("Test_Add_Int64_LargestNeg_Int64_Neg1 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Addition operation using signed 64-bit integers, 
        /// Inputs: 0, Large +ve, 
        /// Result: Large +ve
        /// </summary>
        /// <remarks>
        /// <para>
        /// When adding 64-bit values, care must be taken to handle the carry-bit correctly
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Add_Int64_Zero_Int64_LargePos()
        {
            Int64 a = 0;
            Int64 b = 844424930131968;
            a = a + b;
            if (a == 844424930131968)
            {
                Log.WriteSuccess("Test_Add_Int64_Zero_Int64_LargePos okay.");
            }
            else
            {
                Log.WriteError("Test_Add_Int64_Zero_Int64_LargePos NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Addition operation using signed 64-bit integers, 
        /// Inputs: 0, Large -ve, 
        /// Result: Large -ve
        /// </summary>
        /// <remarks>
        /// <para>
        /// When adding 64-bit values, care must be taken to handle the carry-bit correctly
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Add_Int64_Zero_Int64_LargeNeg()
        {
            Int64 a = 0;
            Int64 b = -844424930131968;
            a = a + b;
            if (a == -844424930131968)
            {
                Log.WriteSuccess("Test_Add_Int64_Zero_Int64_LargeNeg okay.");
            }
            else
            {
                Log.WriteError("Test_Add_Int64_Zero_Int64_LargeNeg NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Addition operation using signed 64-bit integers, 
        /// Inputs: Large -ve, Large -ve, 
        /// Result: Large -ve
        /// </summary>
        /// <remarks>
        /// <para>
        /// When adding 64-bit values, care must be taken to handle the carry-bit correctly
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Add_Int64_LargeNeg_Int64_LargeNeg()
        {
            Int64 a = -1080863910568919040;
            Int64 b = -844424930131968;
            a = a + b;
            if (a == -1081708335499051008)
            {
                Log.WriteSuccess("Test_Add_Int64_LargeNeg_Int64_LargeNeg okay.");
            }
            else
            {
                Log.WriteError("Test_Add_Int64_LargeNeg_Int64_LargeNeg NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Addition operation using signed 64-bit integers, 
        /// Inputs: Large +ve, Large -ve, 
        /// Result: Large +ve
        /// </summary>
        /// <remarks>
        /// <para>
        /// When adding 64-bit values, care must be taken to handle the carry-bit correctly
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Add_Int64_LargePos_Int64_LargeNeg()
        {
            Int64 a = 1080863910568919040;
            Int64 b = -844424930131968;
            a = a + b;
            if (a == 1080019485638787072)
            {
                Log.WriteSuccess("Test_Add_Int64_LargePos_Int64_LargeNeg okay.");
            }
            else
            {
                Log.WriteError("Test_Add_Int64_LargePos_Int64_LargeNeg NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Addition operation using signed 64-bit integers, 
        /// Inputs: Large +ve, Large +ve, 
        /// Result: Large +ve
        /// </summary>
        /// <remarks>
        /// <para>
        /// When adding 64-bit values, care must be taken to handle the carry-bit correctly
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Add_Int64_LargePos_Int64_LargePos()
        {
            Int64 a = 1080863910568919040;
            Int64 b = 844424930131968;
            a = a + b;
            if (a == 1081708335499051008)
            {
                Log.WriteSuccess("Test_Add_Int64_LargePos_Int64_LargePos okay.");
            }
            else
            {
                Log.WriteError("Test_Add_Int64_LargePos_Int64_LargePos NOT okay.");
            }
        }

        #endregion

        #region Switch

        /// <summary>
        /// Tests: Switch statement using signed 32-bit integers, 
        /// Inputs: 0, 1, 2, 
        /// Result: Case 0
        /// </summary>
        [NoGC]
        public static void Test_Switch_Int32_Case_0()
        {
            Int32 a = 0;
            Int32 b = 1;
            Int32 c = 2;
            int res = a;
            switch (res)
            {
                case 0:
                    Log.WriteSuccess("Test_Switch_Int32_Case_0 okay.");
                    break;
                case 1:
                    Log.WriteError("Test_Switch_Int32_Case_0 NOT okay.");
                    break;
                case 2:
                    Log.WriteError("Test_Switch_Int32_Case_0 NOT okay.");
                    break;
                default:
                    Log.WriteError("Test_Switch_Int32_Case_0 NOT okay.");
                    break;
            }
        }

        /// <summary>
        /// Tests: Switch statement using signed 32-bit integers, 
        /// Inputs: 0, 1, 2, 
        /// Result: Case 1
        /// </summary>
        [NoGC]
        public static void Test_Switch_Int32_Case_1()
        {
            Int32 a = 0;
            Int32 b = 1;
            Int32 c = 2;
            int res = b;
            switch (res)
            {
                case 0:
                    Log.WriteError("Test_Switch_Int32_Case_1 NOT okay.");
                    break;
                case 1:
                    Log.WriteSuccess("Test_Switch_Int32_Case_1 okay.");
                    break;
                case 2:
                    Log.WriteError("Test_Switch_Int32_Case_1 NOT okay.");
                    break;
                default:
                    Log.WriteError("Test_Switch_Int32_Case_1 NOT okay.");
                    break;
            }
        }

        /// <summary>
        /// Tests: Switch statement using signed 32-bit integers, 
        /// Inputs: 0, 1, 2, 
        /// Result: Case 2
        /// </summary>
        [NoGC]
        public static void Test_Switch_Int32_Case_2()
        {
            Int32 a = 0;
            Int32 b = 1;
            Int32 c = 2;
            int res = c;
            switch (res)
            {
                case 0:
                    Log.WriteError("Test_Switch_Int32_Case_2 NOT okay.");
                    break;
                case 1:
                    Log.WriteError("Test_Switch_Int32_Case_2 NOT okay.");
                    break;
                case 2:
                    Log.WriteSuccess("Test_Switch_Int32_Case_2 okay.");
                    break;
                default:
                    Log.WriteError("Test_Switch_Int32_Case_2 NOT okay.");
                    break;
            }
        }

        /// <summary>
        /// Tests: Switch statement using signed 32-bit integers, 
        /// Inputs: 0, 1, 2, 
        /// Result: Case default
        /// </summary>
        [NoGC]
        public static void Test_Switch_Int32_Case_Default()
        {
            Int32 a = 0;
            Int32 b = 1;
            Int32 c = 2;
            int res = a + b + c;
            switch (res)
            {
                case 0:
                    Log.WriteError("Test_Switch_Int32_Case_Default NOT okay.");
                    break;
                case 1:
                    Log.WriteError("Test_Switch_Int32_Case_Default NOT okay.");
                    break;
                case 2:
                    Log.WriteError("Test_Switch_Int32_Case_Default NOT okay.");
                    break;
                default:
                    Log.WriteSuccess("Test_Switch_Int32_Case_Default okay.");
                    break;
            }
        }

        /// <summary>
        /// Tests: Switch statement using signed 32-bit integers and return statement with no value, 
        /// Inputs: 0, 1, 2, 
        /// Result: Case 0
        /// </summary>
        [NoGC]
        public static void Test_Switch_Int32_Case_0_Ret_NoValue()
        {
            Int32 a = 0;
            Int32 b = 1;
            Int32 c = 2;
            int res = a;
            switch (res)
            {
                case 0:
                    Log.WriteSuccess("Test_Switch_Int32_Case_0_Ret_NoValue okay.");
                    return;
                case 1:
                    Log.WriteError("Test_Switch_Int32_Case_0_Ret_NoValue NOT okay.");
                    return;
                case 2:
                    Log.WriteError("Test_Switch_Int32_Case_0_Ret_NoValue NOT okay.");
                    return;
                default:
                    Log.WriteError("Test_Switch_Int32_Case_0_Ret_NoValue NOT okay.");
                    return;
            }
        }

        /// <summary>
        /// Tests: Switch statement using signed 32-bit integers and return statement with value, 
        /// Inputs: 0, 1, 2, 
        /// Result: Case 0
        /// </summary>
        [NoGC]
        public static int Test_Switch_Int32_Case_0_Ret_IntValue()
        {
            Int32 a = 0;
            Int32 b = 1;
            Int32 c = 2;
            int res = a;
            switch (res)
            {
                case 0:
                    Log.WriteSuccess("Test_Switch_Int32_Case_0_Ret_IntValue okay.");
                    return 0;
                case 1:
                    Log.WriteError("Test_Switch_Int32_Case_0_Ret_IntValue NOT okay.");
                    return 0;
                case 2:
                    Log.WriteError("Test_Switch_Int32_Case_0_Ret_IntValue NOT okay.");
                    return 0;
                default:
                    Log.WriteError("Test_Switch_Int32_Case_0_Ret_IntValue NOT okay.");
                    return 0;
            }
        }

        /// <summary>
        /// Tests: Switch statement using signed 32-bit integers and return statement with value, 
        /// Inputs: 0, 1, 2, 
        /// Result: Case 0
        /// </summary>
        [NoGC]
        public static string Test_Switch_Int32_Case_0_Ret_StringValue()
        {
            Int32 a = 0;
            Int32 b = 1;
            Int32 c = 2;
            int res = a;
            switch (res)
            {
                case 0:
                    Log.WriteSuccess("Test_Switch_Int32_Case_0_Ret_StringValue okay.");
                    return "I shall return";
                case 1:
                    Log.WriteError("Test_Switch_Int32_Case_0_Ret_StringValue NOT okay.");
                    return "I shall return";
                case 2:
                    Log.WriteError("Test_Switch_Int32_Case_0_Ret_StringValue NOT okay.");
                    return "I shall return";
                default:
                    Log.WriteError("Test_Switch_Int32_Case_0_Ret_StringValue NOT okay.");
                    return "I shall return";
            }
        }

        /// <summary>
        /// Tests: Switch statement using strings, 
        /// Inputs: "zero", "one", "two", 
        /// Result: Case 0
        /// </summary>
        /// <remarks>
        /// <para>
        /// Standard strings are allowed to be used in a switch statement but FlingOS does not use the .NET framework to generate strings. 
        /// FlingOS's string type is an object type which is not allowed to be used in a switch statement by C#. 
        /// Therefore in FlingOS, strings cannot be used in a switch statement. 
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Switch_String_Case_0()
        {
            Log.WriteLine("  Test_Switch_String_Case_0() is not allowed, see remarks.");
            return;

            string a = "zero";
            string b = "one";
            string c = "two";
            string res = a;
            switch (res)
            {
                case "zero":
                    Log.WriteSuccess("Test_Switch_String_Case_0 okay.");
                    break;
                case "one":
                    Log.WriteError("Test_Switch_String_Case_0 NOT okay.");
                    break;
                case "two":
                    Log.WriteError("Test_Switch_String_Case_0 NOT okay.");
                    break;
                default:
                    Log.WriteError("Test_Switch_String_Case_0 NOT okay.");
                    break;
            }
        }

        #endregion

        #region Argument

        /// <summary>
        /// Tests: Passing signed 32-bit integer argument to method, 
        /// Input: Small, 
        /// Result: Argument correctly passed to method.
        /// </summary>
        [NoGC]
        public static void Test_Arg_Int32(Int32 a)
        {
            if (a == 6)
            {
                Log.WriteSuccess("Test_Arg_Int32 okay.");
            }
            else
            {
                Log.WriteError("Test_Arg_Int32 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Passing signed 64-bit integer argument to method, 
        /// Input: Large, 
        /// Result: Argument correctly passed to method.
        /// </summary>
        [NoGC]
        public static void Test_Arg_Int64(Int64 a)
        {
            if (a == 1441151880758558720)
            {
                Log.WriteSuccess("Test_Arg_Int64 okay.");
            }
            else
            {
                Log.WriteError("Test_Arg_Int64 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Passing unsigned 32-bit integer argument to method, 
        /// Input: Small, 
        /// Result: Argument correctly passed to method.
        /// </summary>
        [NoGC]
        public static void Test_Arg_UInt32(UInt32 a)
        {
            if (a == 100)
            {
                Log.WriteSuccess("Test_Arg_UInt32 okay.");
            }
            else
            {
                Log.WriteError("Test_Arg_UInt32 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Passing unsigned 64-bit integer argument to method, 
        /// Input: Large, 
        /// Result: Argument correctly passed to method.
        /// </summary>
        [NoGC]
        public static void Test_Arg_UInt64(UInt64 a)
        {
            if (a == 10223372036854775807)
            {
                Log.WriteSuccess("Test_Arg_UInt64 okay.");
            }
            else
            {
                Log.WriteError("Test_Arg_UInt64 NOT okay.");
            }
        }

        /// <summary>
        /// Tests: Passing string argument to method, 
        /// Input: A string, 
        /// Result: Argument correctly passed to method.
        /// </summary>
        [NoGC]
        public static void Test_Arg_String(FlingOops.String a)
        {
            if (a == "I am a string")
            {
                Log.WriteSuccess("Test_Arg_String okay.");
            }
            else
            {
                Log.WriteError("Test_Arg_String NOT okay.");
            }
        }

        #endregion

        #region Arrays

        /// <summary>
        /// Tests: Array declaration using signed 32-bit elements, 
        /// Input: An array with four elements, 
        /// Result: Correct values for each element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// FlingOS does allow array declaration of the form: 
        /// int[] array = new int[4] {5, 10, 15, 20} or 
        /// int[] array = new int[] {5, 10, 15, 20}. 
        /// Array elements must be explicitly declared as in this test case. 
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Array_Int32()
        {
            Int32[] array = new Int32[4];
            array[0] = 5;
            array[1] = -10;
            array[2] = -15;
            array[3] = 20;
            Int32 a = array.Length;
            if (a == 4)
            {
                Log.WriteSuccess("Test_Array_Length_Int32 okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Length_Int32 NOT okay.");
            }
            if (array[0] == 5)
            {
                Log.WriteSuccess("Test_Array_Decl_Int32[0] okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_Int32[0] Not okay.");
            }

            if (array[1] == -10)
            {
                Log.WriteSuccess("Test_Array_Decl_Int32[1] okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_Int32[1] Not okay");
            }

            if (array[2] == -15)
            {
                Log.WriteSuccess("Test_Array_Decl_Int32[2] okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_Int32[2] Not okay");
            }

            if (array[3] == 20)
            {
                Log.WriteSuccess("Test_Array_Decl_Int32[3] okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_Int32[3] Not okay");
            }
        }

        /// <summary>
        /// Tests: Array declaration using signed 64-bit elements, 
        /// Input: An array with four elements, 
        /// Result: Correct values for each element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// FlingOS does allow array declaration of the form: 
        /// int[] array = new int[4] {5, 10, 15, 20} or 
        /// int[] array = new int[] {5, 10, 15, 20}. 
        /// Array elements must be explicitly declared as in this test case. 
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Array_Int64()
        {
            Int64[] array = new Int64[4];
            array[0] = 4611686018427387903;
            array[1] = -4611686018427387905;
            array[2] = -15;
            array[3] = 20;
            Int32 a = array.Length;
            if (a == 4)
            {
                Log.WriteSuccess("Test_Array_Length_Int64 okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Length_Int64 NOT okay.");
            }
            if (array[0] == 4611686018427387903)
            {
                Log.WriteSuccess("Test_Array_Decl_Int64[0] okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_Int64[0] Not okay.");
            }

            if (array[1] == -4611686018427387905)
            {
                Log.WriteSuccess("Test_Array_Decl_Int64[1] okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_Int64[1] Not okay");
            }

            if (array[2] == -15)
            {
                Log.WriteSuccess("Test_Array_Decl_Int64[2] okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_Int64[2] Not okay");
            }

            if (array[3] == 20)
            {
                Log.WriteSuccess("Test_Array_Decl_Int64[3] okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_Int64[3] Not okay");
            }
        }

        /// <summary>
        /// Tests: Array declaration using unsigned 64-bit elements, 
        /// Input: An array with four elements, 
        /// Result: Correct values for each element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// FlingOS does allow array declaration of the form: 
        /// int[] array = new int[4] {5, 10, 15, 20} or 
        /// int[] array = new int[] {5, 10, 15, 20}. 
        /// Array elements must be explicitly declared as in this test case. 
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Array_UInt64()
        {
            UInt64[] array = new UInt64[4];
            array[0] = 4611686018427387903;
            array[1] = 18446744073709551615;
            array[2] = 0;
            array[3] = 20;
            Int32 a = array.Length;
            if (a == 4)
            {
                Log.WriteSuccess("Test_Array_Length_UInt64 okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Length_UInt64 NOT okay.");
            }
            if (array[0] == 4611686018427387903)
            {
                Log.WriteSuccess("Test_Array_Decl_UInt64[0] okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_UInt64[0] Not okay.");
            }

            if (array[1] == 18446744073709551615)
            {
                Log.WriteSuccess("Test_Array_Decl_UInt64[1] okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_UInt64[1] Not okay");
            }

            if (array[2] == 0)
            {
                Log.WriteSuccess("Test_Array_Decl_UInt64[2] okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_UInt64[2] Not okay");
            }

            if (array[3] == 20)
            {
                Log.WriteSuccess("Test_Array_Decl_UInt64[3] okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_UInt64[3] Not okay");
            }
        }

        /// <summary>
        /// Tests: Array declaration using usigned 32-bit elements, 
        /// Input: An array with four elements, 
        /// Result: Correct values for each element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// FlingOS does allow array declaration of the form: 
        /// int[] array = new int[4] {5, 10, 15, 20} or 
        /// int[] array = new int[] {5, 10, 15, 20}. 
        /// Array elements must be explicitly declared as in this test case. 
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Array_UInt32()
        {
            UInt32[] array = new UInt32[4];
            array[0] = 4294967295;
            array[1] = 4294967294;
            array[2] = 0;
            array[3] = 20;
            Int32 a = array.Length;
            if (a == 4)
            {
                Log.WriteSuccess("Test_Array_Length_UInt32 okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Length_UInt32 NOT okay.");
            }
            if (array[0] == 4294967295)
            {
                Log.WriteSuccess("Test_Array_Decl_UInt32[0] okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_UInt32[0] Not okay.");
            }

            if (array[1] == 4294967294)
            {
                Log.WriteSuccess("Test_Array_Decl_UInt32[1] okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_UInt32[1] Not okay");
            }

            if (array[2] == 0)
            {
                Log.WriteSuccess("Test_Array_Decl_UInt32[2] okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_UInt32[2] Not okay");
            }

            if (array[3] == 20)
            {
                Log.WriteSuccess("Test_Array_Decl_UInt32[3] okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_UInt32[3] Not okay");
            }
        }

        /// <summary>
        /// Tests: Array declaration using strings as elements, 
        /// Input: An array with four elements, 
        /// Result: Correct values for each element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// FlingOS does allow array declaration of the form: 
        /// int[] array = new int[4] {5, 10, 15, 20} or 
        /// int[] array = new int[] {5, 10, 15, 20}. 
        /// Array elements must be explicitly declared as in this test case. 
        /// To declare an array of strings, we need to use the FlingOS built-in string type, NOT just string because that is part of .NET.
        /// </para>
        /// </remarks>
        [NoGC]
        public static void Test_Array_String()
        {
            FlingOops.String[] array = new FlingOops.String[4];
            array[0] = "elementZero";
            array[1] = "elementOne";
            array[2] = "elementTwo";
            array[3] = "elementThree";
            Int32 a = array.Length;
            if (a == 4)
            {
                Log.WriteSuccess("Test_Array_Length_String okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Length_String NOT okay.");
            }
            if (array[0] == "elementZero")
            {
                Log.WriteSuccess("Test_Array_Decl_String[0] okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_String[0] Not okay.");
            }

            if (array[1] == "elementOne")
            {
                Log.WriteSuccess("Test_Array_Decl_String[1] okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_String[1] Not okay");
            }

            if (array[2] == "elementTwo")
            {
                Log.WriteSuccess("Test_Array_Decl_String[2] okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_String[2] Not okay");
            }

            if (array[3] == "elementThree")
            {
                Log.WriteSuccess("Test_Array_Decl_String[3] okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_String[3] Not okay");
            }
        }

        /// <summary>
        /// Tests: Array declaration using structs as elements, 
        /// Input: An array with four elements, 
        /// Result: Correct values for each element.
        /// </summary>
        /// <remarks>
        /// <para>
        /// FlingOS does allow array declaration of the form: 
        /// int[] array = new int[4] {5, 10, 15, 20} or 
        /// int[] array = new int[] {5, 10, 15, 20}. 
        /// Array elements must be explicitly declared as in this test case. 
        /// To declare an array of strings, we need to use the FlingOS built-in string type, NOT just string because that is part of .NET.
        /// </para>
        /// </remarks>
        [NoGC]
        public static unsafe void Test_Array_Struct()
        {
            AStruct[] array = new AStruct[3];
            array[0].a = 1;
            array[0].b = 2;
            array[0].c = 4;
            array[0].d = 8;
            array[1].a = 10;
            array[1].b = 20;
            array[1].c = 40;
            array[1].d = 80;
            array[2].a = 100;
            array[2].b = 200;
            array[2].c = 400;
            array[2].d = 800;
            Int32 a = array.Length;
            if (a == 3)
            {
                Log.WriteSuccess("Test_Array_Length_Struct okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Length_Struct NOT okay.");
            }
            if (array[0].a == 1)
            {
                Log.WriteSuccess("Test_Array_Decl_Struct[0].a okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_Struct[0].a Not okay.");
            }

            if (array[0].b == 2)
            {
                Log.WriteSuccess("Test_Array_Decl_Struct[0].b okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_Struct[0].b Not okay");
            }

            if (array[0].c == 4)
            {
                Log.WriteSuccess("Test_Array_Decl_Struct[0].c okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_Struct[0].c Not okay");
            }

            if (array[0].d == 8)
            {
                Log.WriteSuccess("Test_Array_Decl_Struct[0].d okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_Struct[0].d Not okay");
            }
            if (array[1].a == 10)
            {
                Log.WriteSuccess("Test_Array_Decl_Struct[1].a okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_Struct[1].a Not okay.");
            }

            if (array[1].b == 20)
            {
                Log.WriteSuccess("Test_Array_Decl_Struct[1].b okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_Struct[1].b Not okay");
            }

            if (array[1].c == 40)
            {
                Log.WriteSuccess("Test_Array_Decl_Struct[1].c okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_Struct[1].c Not okay");
            }

            if (array[1].d == 80)
            {
                Log.WriteSuccess("Test_Array_Decl_Struct[1].d okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_Struct[1].d Not okay");
            }
            if (array[2].a == 100)
            {
                Log.WriteSuccess("Test_Array_Decl_Struct[2].a okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_Struct[2].a Not okay.");
            }

            if (array[2].b == 200)
            {
                Log.WriteSuccess("Test_Array_Decl_Struct[2].b okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_Struct[2].b Not okay");
            }

            if (array[2].c == 400)
            {
                Log.WriteSuccess("Test_Array_Decl_Struct[2].c okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_Struct[2].c Not okay");
            }

            if (array[2].d == 800)
            {
                Log.WriteSuccess("Test_Array_Decl_Struct[2].d okay.");
            }
            else
            {
                Log.WriteError("Test_Array_Decl_Struct[2].d Not okay");
            }
        }

        #endregion

        #region Heaps

        /// <summary>
        /// Tests: Heap management, 
        /// Inputs:A struct, 
        /// Result: Struct allocated on heap correctly. 
        /// </summary>
        [NoGC]
        public static unsafe void Test_Heap()
        {
            AStruct* HeapInst = (AStruct*)Heap.AllocZeroed((uint)sizeof(AStruct), "Kernel:Main");
            if (HeapInst == null)
            {
                Log.WriteError("HeapInst null.");
            }
            else
            {
                Log.WriteSuccess("HeapInst not null.");
            }
            HeapInst->a = 1;
            HeapInst->b = 2;
            HeapInst->c = 4;
            HeapInst->d = 8;
            if (HeapInst->a == 1)
            {
               Log.WriteSuccess("HeapInst->a not null.");
            }
            else
            {
                Log.WriteError("HeapInst->a null.");
            }
            if (HeapInst->b == 2)
            {
                Log.WriteSuccess("HeapInst->b not null.");
            }
            else
            {
                Log.WriteError("HeapInst->b null.");
            }
            if (HeapInst->c == 4)
            {
                Log.WriteSuccess("HeapInst->c not null.");
            }
            else
            {
                Log.WriteError("HeapInst->c null.");
            }
            if (HeapInst->d == 8)
            {
                Log.WriteSuccess("HeapInst->d not null");
            }
            else
            {
                Log.WriteError("HeapInst->d null.");
            }
        }

        #endregion

        #region Strings

        /// <summary>
        /// Tests: String operations, 
        /// Inputs: Character strings, 
        /// Result: Strings correctly stored and displayed.
        /// </summary>
        /// <remarks> 
        /// <para>
        /// In testing kernel, strings must be declaired as FlingOops.String to use the built-in string type of FlingOS. 
        /// Integer value is displayed as a hexadecimal in console.
        /// </para>
        /// </remarks>
        [NoGC]
        public static unsafe void Test_Strings()
        {
            Int32 a = 5;
            Log.WriteLine("Test Console write line!");
            Log.WriteLine(" ");
            FlingOops.String ATestString = "Hello, world!";
            Log.WriteLine("Display stored string ATestString:");
            Log.WriteLine(ATestString);
            Log.WriteLine(" ");
            if (ATestString != "Hello, world!")
            {
                Log.WriteError("String equality does not work!");
            }
            else
            {
                Log.WriteSuccess("String equality works.");
            }
            ATestString += " But wait! There's more...";
            Log.WriteLine(" ");
            Log.WriteLine("Concatenate to ATestString:");
            Log.WriteLine(ATestString);
            Log.WriteLine(" ");
            ATestString += " We can even append numbers: " + (FlingOops.String)a;
            Log.WriteLine("Concatenate value stored in variable to ATestString:");
            Log.WriteLine(ATestString);
        }

        #endregion

        #region Object



        #endregion

    }
}
