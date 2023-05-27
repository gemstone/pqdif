//******************************************************************************************************
//  Equipment.cs - Gbtc
//
//  Copyright © 2014, Grid Protection Alliance.  All Rights Reserved.
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
//  05/08/2014 - Stephen C. Wills
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace Gemstone.PQDIF.Logical
{
    /// <summary>
    /// Power quality equipment and software.
    /// </summary>
    public static class Equipment
    {
        /// <summary>
        /// The ID of the WPT 5530 device.
        /// </summary>
        public static Guid WPT5530 { get; } = new("e2da5083-7fdb-11d3-9b39-0040052c2d28");

        /// <summary>
        /// The ID of the WPT 5540 device.
        /// </summary>
        public static Guid WPT5540 { get; } = new("e2da5084-7fdb-11d3-9b39-0040052c2d28");

        /// <summary>
        /// The ID of the BMI 3100 device.
        /// </summary>
        public static Guid BMI3100 { get; } = new("f1c04780-50fb-11d3-ac3e-444553540000");

        /// <summary>
        /// The ID of the BMI 7100 device.
        /// </summary>
        public static Guid BMI7100 { get; } = new("e6b51717-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the BMI 7100 device.
        /// </summary>
        public static Guid BMI8010 { get; } = new("e6b51718-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the BMI 7100 device.
        /// </summary>
        public static Guid BMI8020 { get; } = new("e6b51719-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the BMI 7100 device.
        /// </summary>
        public static Guid BMI9010 { get; } = new("e6b5171a-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the Cooper V-Harm device.
        /// </summary>
        public static Guid CooperVHarm { get; } = new("e6b5171b-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the Cooper V-Flicker device.
        /// </summary>
        public static Guid CooperVFlicker { get; } = new("e6b5171c-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the DCG EMTP device.
        /// </summary>
        public static Guid DCGEMTP { get; } = new("e6b5171d-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the Dranetz 656 device.
        /// </summary>
        public static Guid Dranetz656 { get; } = new("e6b5171e-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the Dranetz 658 device.
        /// </summary>
        public static Guid Dranetz658 { get; } = new("e6b5171f-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the Electrotek test program.
        /// </summary>
        public static Guid ETKTestProgram { get; } = new("e6b51721-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the Dranetz 8000 device.
        /// </summary>
        public static Guid Dranetz8000 { get; } = new("e6b51720-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the Electrotek PQDIF editor.
        /// </summary>
        public static Guid ETKPQDIFEditor { get; } = new("e6b51722-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of Electrotek PASS.
        /// </summary>
        public static Guid ETKPASS { get; } = new("e6b51723-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of Electrotek Super-Harm.
        /// </summary>
        public static Guid ETKSuperHarm { get; } = new("e6b51724-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of Electrotek Super-Tran.
        /// </summary>
        public static Guid ETKSuperTran { get; } = new("e6b51725-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of Electrotek TOP.
        /// </summary>
        public static Guid ETKTOP { get; } = new("e6b51726-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of Electrotek PQView.
        /// </summary>
        public static Guid ETKPQView { get; } = new("e6b51727-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of Electrotek Harmoni.
        /// </summary>
        public static Guid ETKHarmoni { get; } = new("e6b51728-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the Fluke CUR device.
        /// </summary>
        public static Guid FlukeCUR { get; } = new("e6b51729-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of IEEE COMTRADE.
        /// </summary>
        public static Guid IEEECOMTRADE { get; } = new("e6b5172b-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the Fluke F41 device.
        /// </summary>
        public static Guid FlukeF41 { get; } = new("e6b5172a-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of public ATP.
        /// </summary>
        public static Guid PublicATP { get; } = new("e6b5172c-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the Metrosonic M1 device.
        /// </summary>
        public static Guid MetrosonicM1 { get; } = new("e6b5172d-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the Square D PowerLogic SMS device.
        /// </summary>
        public static Guid SQDSMS { get; } = new("e6b5172e-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the Telog M1 device.
        /// </summary>
        public static Guid TelogM1 { get; } = new("e6b5172f-f747-11cf-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the PML 3710 device.
        /// </summary>
        public static Guid PML3710 { get; } = new("085726d0-1dc0-11d0-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the PML 3720 device.
        /// </summary>
        public static Guid PML3720 { get; } = new("085726d1-1dc0-11d0-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the PML 3800 device.
        /// </summary>
        public static Guid PML3800 { get; } = new("085726d2-1dc0-11d0-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the PML 7300 device.
        /// </summary>
        public static Guid PML7300 { get; } = new("085726d3-1dc0-11d0-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the PML 7700 device.
        /// </summary>
        public static Guid PML7700 { get; } = new("085726d4-1dc0-11d0-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the PML VIP device.
        /// </summary>
        public static Guid PMLVIP { get; } = new("085726d5-1dc0-11d0-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the PML Log Server.
        /// </summary>
        public static Guid PMLLogServer { get; } = new("085726d6-1dc0-11d0-9d89-0080c72e70a3");

        /// <summary>
        /// The ID of the Met One ELT15 device.
        /// </summary>
        public static Guid MetOneELT15 { get; } = new("b5b5da62-e2e1-11d4-a4b3-444553540000");

        /// <summary>
        /// The ID of the PMI scanner.
        /// </summary>
        public static Guid PMIScanner { get; } = new("609acec1-993d-11d4-a4b3-444553540000");

        /// <summary>
        /// The ID of the AdvanTech ADAM 4017 device.
        /// </summary>
        public static Guid AdvanTechADAM4017 { get; } = new("92b7977b-0c02-4766-95cf-dd379caeb417");

        /// <summary>
        /// The ID of ETK DSS.
        /// </summary>
        public static Guid ETKDSS { get; } = new("d347ba66-e34c-11d4-82d9-00e09872a094");

        /// <summary>
        /// The ID of the AdvanTech ADAM 4018 device.
        /// </summary>
        public static Guid AdvanTechADAM4018 { get; } = new("3008151e-2317-4405-a59e-e7b3b20667a9");

        /// <summary>
        /// The ID of the AdvanTech ADAM 4018M device.
        /// </summary>
        public static Guid AdvanTechADAM4018M { get; } = new("3a1af807-1347-45f8-966a-f481c6ae208e");

        /// <summary>
        /// The ID of the AdvanTech ADAM 4052 device.
        /// </summary>
        public static Guid AdvanTechADAM4052 { get; } = new("8bba416b-a7ec-4616-8b8f-59fed749323d");

        /// <summary>
        /// The ID of the BMI 8800 device.
        /// </summary>
        public static Guid BMI8800 { get; } = new("e77d1a81-1235-11d5-a390-0010a4924ecc");

        /// <summary>
        /// The ID of the Trinergi Power Quality Meter.
        /// </summary>
        public static Guid TrinergiPQM { get; } = new("0fd5a3aa-d73a-11d2-ac3e-444553540000");

        /// <summary>
        /// The ID of the Medcal device.
        /// </summary>
        public static Guid Medcal { get; } = new("f3bfa0a1-eb87-11d2-ac3e-444553540000");

        /// <summary>
        /// The ID of the GE kV Energy Meter.
        /// </summary>
        public static Guid GEKV { get; } = new("5202bd01-245c-11d5-a4b3-444553540000");

        /// <summary>
        /// The ID of the GE kV2 Energy Meter.
        /// </summary>
        public static Guid GEKV2 { get; } = new("5202bd03-245c-11d5-a4b3-444553540000");

        /// <summary>
        /// The ID of the Acumentrics Control device.
        /// </summary>
        public static Guid AcumentricsControl { get; } = new("5202bd04-245c-11d5-a4b3-444553540000");

        /// <summary>
        /// The ID of Electrotek Text PQDIF.
        /// </summary>
        public static Guid ETKTextPQDIF { get; } = new("5202bd05-245c-11d5-a4b3-444553540000");

        /// <summary>
        /// The ID of Electrotek PQWeb.
        /// </summary>
        public static Guid ETKPQWeb { get; } = new("5202bd06-245c-11d5-a4b3-444553540000");

        /// <summary>
        /// The ID of the QWave Power Distribution device.
        /// </summary>
        public static Guid QWavePowerDistribution { get; } = new("80c4a723-2816-11d4-8ab4-004005698d26");

        /// <summary>
        /// The ID of the QWave Power Transmission device.
        /// </summary>
        public static Guid QWavePowerTransmission { get; } = new("80c4a725-2816-11d4-8ab4-004005698d26");

        /// <summary>
        /// The ID of the QWave Micro device.
        /// </summary>
        public static Guid QWaveMicro { get; } = new("80c4a727-2816-11d4-8ab4-004005698d26");

        /// <summary>
        /// The ID of the QWave TWin device.
        /// </summary>
        public static Guid QWaveTWin { get; } = new("80c4a728-2816-11d4-8ab4-004005698d26");

        /// <summary>
        /// The ID of the QWave Premium device.
        /// </summary>
        public static Guid QWavePremium { get; } = new("80c4a729-2816-11d4-8ab4-004005698d26");

        /// <summary>
        /// The ID of the QWave Light device.
        /// </summary>
        public static Guid QWaveLight { get; } = new("80c4a72a-2816-11d4-8ab4-004005698d26");

        /// <summary>
        /// The ID of the QWave Nomad device.
        /// </summary>
        public static Guid QWaveNomad { get; } = new("80c4a72b-2816-11d4-8ab4-004005698d26");

        /// <summary>
        /// The ID of the EWON 4000 device.
        /// </summary>
        public static Guid EWON4000 { get; } = new("80c4a762-2816-11d4-8ab4-004005698d26");

        /// <summary>
        /// The ID of the Qualimetre device.
        /// </summary>
        public static Guid Qualimetre { get; } = new("80c4a764-2816-11d4-8ab4-004005698d26");

        /// <summary>
        /// The ID of the LEM Analyst 3Q device.
        /// </summary>
        public static Guid LEMAnalyst3Q { get; } = new("d567cb71-bcc0-41ee-8e8c-35851553f655");

        /// <summary>
        /// The ID of the LEM Analyst 1Q device.
        /// </summary>
        public static Guid LEMAnalyst1Q { get; } = new("477ecb3b-917f-4915-af99-a6c29ac18764");

        /// <summary>
        /// The ID of the LEM Analyst 2050 device.
        /// </summary>
        public static Guid LEMAnalyst2050 { get; } = new("9878ccab-a842-4cac-950f-6d47215bffdf");

        /// <summary>
        /// The ID of the LEM Analyst 2060 device.
        /// </summary>
        public static Guid LEMAnalyst2060 { get; } = new("312471a2-b586-491c-855a-ca05459a7e20");

        /// <summary>
        /// The ID of the LEM Midget 200 device.
        /// </summary>
        public static Guid LEMMidget200 { get; } = new("8449f6b9-10f4-40a7-a1c3-be338eb97422");

        /// <summary>
        /// The ID of the LEM MBX 300 device.
        /// </summary>
        public static Guid LEMMBX300 { get; } = new("d4578d61-df2b-4218-a7b1-5ef1a9bb85fa");

        /// <summary>
        /// The ID of the LEM MBX 800 device.
        /// </summary>
        public static Guid LEMMBX800 { get; } = new("1c14b57a-ba25-47fb-88fa-5fe5cec99e6a");

        /// <summary>
        /// The ID of the LEM MBX 601 device.
        /// </summary>
        public static Guid LEMMBX601 { get; } = new("1f3cda7b-2ce1-4030-a390-e3d49c5615d2");

        /// <summary>
        /// The ID of the LEM MBX 602 device.
        /// </summary>
        public static Guid LEMMBX602 { get; } = new("4a157756-414a-427b-9932-55760ed5f707");

        /// <summary>
        /// The ID of the LEM MBX 603 device.
        /// </summary>
        public static Guid LEMMBX603 { get; } = new("f7b4677b-b277-45b5-aaae-5fb39341b390");

        /// <summary>
        /// The ID of the LEM MBX 686 device.
        /// </summary>
        public static Guid LEMMBX686 { get; } = new("40004266-a978-4991-9ed6-c1cd730f5bf5");

        /// <summary>
        /// The ID of the LEM Perma 701 device.
        /// </summary>
        public static Guid LEMPerma701 { get; } = new("9b0dfd9d-d4e9-419d-ba10-c1cee6cf8f93");

        /// <summary>
        /// The ID of the LEM Perma 702 device.
        /// </summary>
        public static Guid LEMPerma702 { get; } = new("7f5d62ac-9fab-400f-b51a-f0f3941fb5aa");

        /// <summary>
        /// The ID of the LEM Perma 705 device.
        /// </summary>
        public static Guid LEMPerma705 { get; } = new("d85fea9c-14d5-45eb-831f-e03973092bd8");

        /// <summary>
        /// The ID of the LEM Perma 706 device.
        /// </summary>
        public static Guid LEMPerma706 { get; } = new("16d6bbfc-0b5a-4cf0-81cf-48a3105eff4f");

        /// <summary>
        /// The ID of the LEM QWave Micro device.
        /// </summary>
        public static Guid LEMQWaveMicro { get; } = new("e0380e52-c205-43a0-9ff4-76fbd6765f37");

        /// <summary>
        /// The ID of the LEM QWave Nomad device.
        /// </summary>
        public static Guid LEMQWaveNomad { get; } = new("165f145d-90c3-4591-959a-33b101d4bf8b");

        /// <summary>
        /// The ID of the LEM QWave Light device.
        /// </summary>
        public static Guid LEMQWaveLight { get; } = new("5198ceb9-4b4e-439c-a1c0-218c963d6a9c");

        /// <summary>
        /// The ID of the LEM QWave TWin device.
        /// </summary>
        public static Guid LEMQWaveTWin { get; } = new("67a42a2d-b831-4222-805e-d5fdebdd3a46");

        /// <summary>
        /// The ID of the LEM QWave Power Distribution device.
        /// </summary>
        public static Guid LEMQWavePowerDistribution { get; } = new("2401bf48-9db2-46ec-acde-5dedde25e54e");

        /// <summary>
        /// The ID of the LEM QWave Premium device.
        /// </summary>
        public static Guid LEMQWavePremium { get; } = new("6b609a29-4a64-4d1c-16e3-caef94fa56a0");

        /// <summary>
        /// The ID of the LEM QWave Power Transport device.
        /// </summary>
        public static Guid LEMQWavePowerTransport { get; } = new("d4422eeb-b1cd-4ba9-a7c8-5d141df40518");

        /// <summary>
        /// The ID of the LEM TOPAS LT device.
        /// </summary>
        public static Guid LEMTOPASLT { get; } = new("9c46483a-541e-4d66-9c10-f943abfc348a");

        /// <summary>
        /// The ID of the LEM TOPAS 1000 device.
        /// </summary>
        public static Guid LEMTOPAS1000 { get; } = new("459b8614-6724-48fb-b5d4-f149ed0c62f5");

        /// <summary>
        /// The ID of the LEM TOPAS 1019 device.
        /// </summary>
        public static Guid LEMTOPAS1019 { get; } = new("7b11408b-9d2c-407c-84a5-89440218dcf8");

        /// <summary>
        /// The ID of the LEM TOPAS 1020 device.
        /// </summary>
        public static Guid LEMTOPAS1020 { get; } = new("d1def77d-990f-484e-a166-f7921170a64b");

        /// <summary>
        /// The ID of the LEM TOPAS 1040 device.
        /// </summary>
        public static Guid LEMTOPAS1040 { get; } = new("d3cc1de2-6e6b-4b6e-ad90-10d6585f8fa2");

        /// <summary>
        /// The ID of the LEM BEN 5000 device.
        /// </summary>
        public static Guid LEMBEN5000 { get; } = new("a70e32b1-2f1a-4543-a684-78a4b5be34bb");

        /// <summary>
        /// The ID of the LEM BEN 6000 device.
        /// </summary>
        public static Guid LEMBEN6000 { get; } = new("05a4c1b5-6681-47e6-9f64-8da125dbec32");

        /// <summary>
        /// The ID of the LEM EWave device.
        /// </summary>
        public static Guid LEMEWave { get; } = new("e46981d5-708d-4822-97aa-fdb6f73b3af2");

        /// <summary>
        /// The ID of the LEM EWON 4000 device.
        /// </summary>
        public static Guid LEMEWON4000 { get; } = new("d4c0895c-fd48-498a-997c-9e70d80efb06");

        /// <summary>
        /// The ID of the LEM WPT 5510 device.
        /// </summary>
        public static Guid WPT5510 { get; } = new("752871de-0583-4d44-a9ae-c5fadc0144ac");

        /// <summary>
        /// The ID of the LEM WPT 5520 device.
        /// </summary>
        public static Guid WPT5520 { get; } = new("0b72d289-7645-40b8-946e-c3ce4f1bcd37");

        /// <summary>
        /// The ID of the LEM WPT 5530T device.
        /// </summary>
        public static Guid WPT5530T { get; } = new("8f88ea9e-1007-4569-ab47-756f292a23ed");

        /// <summary>
        /// The ID of the LEM WPT 5560 device.
        /// </summary>
        public static Guid WPT5560 { get; } = new("5fd9c0ff-4432-41b5-9a9e-9a32ba2cf005");

        /// <summary>
        /// The ID of the LEM WPT 5590 device.
        /// </summary>
        public static Guid WPT5590 { get; } = new("2861d5ca-23ac-4a51-a5a0-498da61d26dd");

        /// <summary>
        /// The ID of Electrotek Node Center.
        /// </summary>
        public static Guid ETKNodeCenter { get; } = new("c52e8460-58b4-4f1a-8469-69ca3fef9ff1");

        /// <summary>
        /// The ID of WPT Dran View.
        /// </summary>
        public static Guid WPTDranView { get; } = new("08d97aa1-1719-11d6-a4b3-444553540000");

        /// <summary>
        /// The ID of the AdvanTech ADAM 5017 device.
        /// </summary>
        public static Guid AdvanTechADAM5017 { get; } = new("2f46263c-92ac-4717-8a08-a6177df3f611");

        /// <summary>
        /// The ID of the AdvanTech ADAM 5018 device.
        /// </summary>
        public static Guid AdvanTechADAM5018 { get; } = new("cc2d3247-fe65-4db6-8206-500a23151bb2");

        /// <summary>
        /// The ID of the AdvanTech ADAM 5080 device.
        /// </summary>
        public static Guid AdvanTechADAM5080 { get; } = new("6c37b63c-e770-4b85-bd32-4739d6eb9846");

        /// <summary>
        /// The ID of the AdvanTech ADAM 5052 device.
        /// </summary>
        public static Guid AdvanTechADAM5052 { get; } = new("e9261dfe-3d44-47e3-ac36-3b097faa8cda");

        /// <summary>
        /// The ID of the AdvanTech ADAM 4050 device.
        /// </summary>
        public static Guid AdvanTechADAM4050 { get; } = new("9212066d-ea65-477e-bf95-e4a0066d25ce");

        /// <summary>
        /// The ID of the AdvanTech ADAM 4053 device.
        /// </summary>
        public static Guid AdvanTechADAM4053 { get; } = new("dc29b83f-bebe-4cf3-b3fb-00dc63626dd9");

        /// <summary>
        /// The ID of the AdvanTech ADAM 4080 device.
        /// </summary>
        public static Guid AdvanTechADAM4080 { get; } = new("64fc42c6-3c90-4633-99df-2c6058214b72");

        /// <summary>
        /// The ID of the AdvanTech ADAM 5050 device.
        /// </summary>
        public static Guid AdvanTechADAM5050 { get; } = new("c950a2e3-7a35-440c-8660-63f611972519");

        /// <summary>
        /// The ID of the AdvanTech ADAM 5051 device.
        /// </summary>
        public static Guid AdvanTechADAM5051 { get; } = new("c8f92334-a69b-4856-b253-ec2471d137d6");

        /// <summary>
        /// The ID of the ELCOM BK 550 device.
        /// </summary>
        public static Guid ELCOMBK550 { get; } = new("f4380a60-6f1d-11d6-9cb3-0020e010453b");

        /// <summary>
        /// Gets information about the equipment identified by the given ID.
        /// </summary>
        /// <param name="equipmentID">The identifier for the equipment.</param>
        /// <returns>Information about the equipment.</returns>
        public static Identifier? GetInfo(Guid equipmentID) => EquipmentLookup.TryGetValue(equipmentID, out Identifier? identifier)
            ? identifier
            : null;

        /// <summary>
        /// Converts the given equpiment ID to a string containing the name of the equipment.
        /// </summary>
        /// <param name="equipmentID">The ID of the equipment to be converted to a string.</param>
        /// <returns>A string containing the name of the equipment with the given ID.</returns>
        public static string ToString(Guid equipmentID) =>
            GetInfo(equipmentID)?.Name ?? equipmentID.ToString();

        private static Dictionary<Guid, Identifier> EquipmentLookup
        {
            get
            {
                Tag? equipmentTag = Tag.GetTag(DataSourceRecord.EquipmentIDTag);

                if (s_equipmentTag != equipmentTag)
                {
                    s_equipmentTag = equipmentTag;
                    s_equipmentLookup = equipmentTag?.ValidIdentifiers.ToDictionary(id => Guid.Parse(id.Value));
                }

                return s_equipmentLookup ?? new Dictionary<Guid, Identifier>();
            }
        }

        private static Tag? s_equipmentTag;
        private static Dictionary<Guid, Identifier>? s_equipmentLookup;
    }
}
