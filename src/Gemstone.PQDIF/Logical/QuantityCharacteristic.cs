//******************************************************************************************************
//  QuantityCharacteristic.cs - Gbtc
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
//  05/20/2014 - Stephen C. Wills
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace Gemstone.PQDIF.Logical
{
    /// <summary>
    /// Specifies additional detail about the meaning of the series data.
    /// </summary>
    public static class QuantityCharacteristic
    {
        /// <summary>
        /// No quantity characteristic.
        /// </summary>
        public static Guid None { get; } = new("a6b31adf-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Instantaneous f(t).
        /// </summary>
        public static Guid Instantaneous { get; } = new("a6b31add-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Spectra F(F).
        /// </summary>
        public static Guid Spectra { get; } = new("a6b31ae9-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Peak value.
        /// </summary>
        public static Guid Peak { get; } = new("a6b31ae2-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// RMS value.
        /// </summary>
        public static Guid RMS { get; } = new("a6b31ae5-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Harmonic RMS.
        /// </summary>
        public static Guid HRMS { get; } = new("a6b31adc-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Frequency.
        /// </summary>
        public static Guid Frequency { get; } = new("07ef68af-9ff5-11d2-b30b-006008b37183");

        /// <summary>
        /// Total harmonic distortion (%).
        /// </summary>
        public static Guid TotalTHD { get; } = new("a6b31aec-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Even harmonic distortion (%).
        /// </summary>
        public static Guid EvenTHD { get; } = new("a6b31ad4-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Odd harmonic distortion (%).
        /// </summary>
        public static Guid OddTHD { get; } = new("a6b31ae0-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Crest factor.
        /// </summary>
        public static Guid CrestFactor { get; } = new("a6b31ad2-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Form factor.
        /// </summary>
        public static Guid FormFactor { get; } = new("a6b31adb-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Arithmetic sum.
        /// </summary>
        public static Guid ArithSum { get; } = new("a6b31ad0-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Zero sequence component unbalance (%).
        /// </summary>
        public static Guid S0S1 { get; } = new("a6b31ae7-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Negative sequence component unbalance (%).
        /// </summary>
        public static Guid S2S1 { get; } = new("a6b31ae8-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Positive sequence component.
        /// </summary>
        public static Guid SPos { get; } = new("a6b31aea-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Negative sequence component.
        /// </summary>
        public static Guid SNeg { get; } = new("d71a4b91-3c92-11d4-9f2c-002078e0b723");

        /// <summary>
        /// Zero sequence component.
        /// </summary>
        public static Guid SZero { get; } = new("d71a4b92-3c92-11d4-9f2c-002078e0b723");

        /// <summary>
        /// Imbalance by max deviation from average.
        /// </summary>
        public static Guid AvgImbal { get; } = new("a6b31ad1-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Total THD normalized to RMS.
        /// </summary>
        public static Guid TotalTHDRMS { get; } = new("f3d216e0-2aa5-11d5-a4b3-444553540000");

        /// <summary>
        /// Odd THD normalized to RMS.
        /// </summary>
        public static Guid OddTHDRMS { get; } = new("f3d216e1-2aa5-11d5-a4b3-444553540000");

        /// <summary>
        /// Even THD normalized to RMS.
        /// </summary>
        public static Guid EvenTHDRMS { get; } = new("f3d216e2-2aa5-11d5-a4b3-444553540000");

        /// <summary>
        /// Total interharmonic distortion.
        /// </summary>
        public static Guid TID { get; } = new("f3d216e3-2aa5-11d5-a4b3-444553540000");

        /// <summary>
        /// Total interharmonic distortion normalized to RMS.
        /// </summary>
        public static Guid TIDRMS { get; } = new("f3d216e4-2aa5-11d5-a4b3-444553540000");

        /// <summary>
        /// Interharmonic RMS.
        /// </summary>
        public static Guid IHRMS { get; } = new("f3d216e5-2aa5-11d5-a4b3-444553540000");

        /// <summary>
        /// Spectra by harmonic group index.
        /// </summary>
        public static Guid SpectraHGroup { get; } = new("53be6ba8-0789-455b-9a95-da128683dda7");

        /// <summary>
        /// Spectra by interharmonic group index.
        /// </summary>
        public static Guid SpectraIGroup { get; } = new("5e51e006-9c95-4c5e-878f-7ca87c0d2a0e");

        /// <summary>
        /// TIF.
        /// </summary>
        public static Guid TIF { get; } = new("a6b31aeb-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Flicker average RMS value.
        /// </summary>
        public static Guid FlkrMagAvg { get; } = new("a6b31ad6-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// dV/V base.
        /// </summary>
        public static Guid FlkrMaxDVV { get; } = new("a6b31ad8-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Frequence of maximum flicker harmonic.
        /// </summary>
        public static Guid FlkrFreqMax { get; } = new("a6b31ad5-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Magnitude of maximum flicker harmonic.
        /// </summary>
        public static Guid FlkrMagMax { get; } = new("a6b31ad7-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Spectrum weighted average.
        /// </summary>
        public static Guid FlkrWgtAvg { get; } = new("a6b31ada-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Flicker spectrum VRMS(F).
        /// </summary>
        public static Guid FlkrSpectrum { get; } = new("a6b31ad9-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Short term flicker.
        /// </summary>
        public static Guid FlkrPST { get; } = new("515bf320-71ca-11d4-a4b3-444553540000");

        /// <summary>
        /// Long term flicker.
        /// </summary>
        public static Guid FlkrPLT { get; } = new("515bf321-71ca-11d4-a4b3-444553540000");

        /// <summary>
        /// TIF normalized to RMS.
        /// </summary>
        public static Guid TIFRMS { get; } = new("f3d216e6-2aa5-11d5-a4b3-444553540000");

        /// <summary>
        /// Sliding PLT.
        /// </summary>
        public static Guid FlkrPLTSlide { get; } = new("2257ec05-06ea-4709-b43a-0c00534d554a");

        /// <summary>
        /// Pi LPF.
        /// </summary>
        public static Guid FlkrPiLPF { get; } = new("4d693eec-5d1d-4531-993a-793b5356c63d");

        /// <summary>
        /// Pi max.
        /// </summary>
        public static Guid FlkrPiMax { get; } = new("126de61c-6691-4d16-8fdf-46482bca4694");

        /// <summary>
        /// Pi root.
        /// </summary>
        public static Guid FlkrPiRoot { get; } = new("e065b621-ffdb-4598-9330-4d09353988b6");

        /// <summary>
        /// Pi root LPF.
        /// </summary>
        public static Guid FlkrPiRootLPF { get; } = new("7d11f283-1ce7-4e58-8af0-79048793b8a7");

        /// <summary>
        /// IT.
        /// </summary>
        public static Guid IT { get; } = new("a6b31ade-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// RMS value of current for a demand interval.
        /// </summary>
        public static Guid RMSDemand { get; } = new("07ef68a0-9ff5-11d2-b30b-006008b37183");

        /// <summary>
        /// Transformer derating factor.
        /// </summary>
        public static Guid ANSITDF { get; } = new("8786ca10-9113-11d3-b930-0050da2b1f4d");

        /// <summary>
        /// Transformer K factor.
        /// </summary>
        public static Guid KFactor { get; } = new("8786ca11-9113-11d3-b930-0050da2b1f4d");

        /// <summary>
        /// Total demand distortion.
        /// </summary>
        public static Guid TDD { get; } = new("f3d216e7-2aa5-11d5-a4b3-444553540000");

        /// <summary>
        /// Peak demand current.
        /// </summary>
        public static Guid RMSPeakDemand { get; } = new("72e82a44-336c-11d5-a4b3-444553540000");

        /// <summary>
        /// Real power (watts).
        /// </summary>
        public static Guid P { get; } = new("a6b31ae1-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Reactive power (VAR).
        /// </summary>
        public static Guid Q { get; } = new("a6b31ae4-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Apparent power (VA).
        /// </summary>
        public static Guid S { get; } = new("a6b31ae6-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// True power factor - (Vrms * Irms) / P.
        /// </summary>
        public static Guid PF { get; } = new("a6b31ae3-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Displacement factor - Cosine of the phase angle between fundamental frequency voltage and current phasors.
        /// </summary>
        public static Guid DF { get; } = new("a6b31ad3-b451-11d1-ae17-0060083a2628");

        /// <summary>
        /// Value of active power for a demand interval.
        /// </summary>
        public static Guid PDemand { get; } = new("07ef68a1-9ff5-11d2-b30b-006008b37183");

        /// <summary>
        /// Value of reactive power for a demand interval.
        /// </summary>
        public static Guid QDemand { get; } = new("07ef68a2-9ff5-11d2-b30b-006008b37183");

        /// <summary>
        /// Value of apparent power for a demand interval.
        /// </summary>
        public static Guid SDemand { get; } = new("07ef68a3-9ff5-11d2-b30b-006008b37183");

        /// <summary>
        /// Value of displacement power factor for a demand interval.
        /// </summary>
        public static Guid DFDemand { get; } = new("07ef68a4-9ff5-11d2-b30b-006008b37183");

        /// <summary>
        /// Value of true power factor for a demand interval.
        /// </summary>
        public static Guid PFDemand { get; } = new("07ef68a5-9ff5-11d2-b30b-006008b37183");

        /// <summary>
        /// Predicted value of active power for current demand interval.
        /// </summary>
        public static Guid PPredDemand { get; } = new("672d0305-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// Predicted value of reactive power for current demand interval.
        /// </summary>
        public static Guid QPredDemand { get; } = new("672d0306-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// Predicted value of apparent power for current demand interval.
        /// </summary>
        public static Guid SPredDemand { get; } = new("672d0307-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// Value of active power coincident with reactive power demand.
        /// </summary>
        public static Guid PCoQDemand { get; } = new("672d030a-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// Value of active power coincident with apparent power demand.
        /// </summary>
        public static Guid PCoSDemand { get; } = new("672d030b-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// Value of reactive power coincident with active power demand.
        /// </summary>
        public static Guid QCoPDemand { get; } = new("672d030d-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// Value of reactive power coincident with apparent power demand.
        /// </summary>
        public static Guid QCoSDemand { get; } = new("672d030e-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// Value of displacement power factor coincident with apparent power demand.
        /// </summary>
        public static Guid DFCoSDemand { get; } = new("07ef68ad-9ff5-11d2-b30b-006008b37183");

        /// <summary>
        /// Value of true power factor coincident with apparent power demand.
        /// </summary>
        public static Guid PFCoSDemand { get; } = new("07ef68ae-9ff5-11d2-b30b-006008b37183");

        /// <summary>
        /// Value of true power factor coincident with active power demand.
        /// </summary>
        public static Guid PFCoPDemand { get; } = new("672d0308-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// Value of true power factor coincident with reactive power demand.
        /// </summary>
        public static Guid PFCoQDemand { get; } = new("672d0309-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// Value of the power angle at fundamental frequency. 
        /// </summary>
        public static Guid AngleFund { get; } = new("672d030f-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// Value of the reactive power at fundamental frequency.
        /// </summary>
        public static Guid QFund { get; } = new("672d0310-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// True power factor - IEEE vector calculations.
        /// </summary>
        public static Guid PFVector { get; } = new("672d0311-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// Displacement factor - IEEE vector calculations.
        /// </summary>
        public static Guid DFVector { get; } = new("672d0312-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// Value of apparent power - IEEE vector calculations.
        /// </summary>
        public static Guid SVector { get; } = new("672d0314-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// Value of fundamental frequency apparent power - IEEE vector calculations.
        /// </summary>
        public static Guid SVectorFund { get; } = new("672d0315-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// Value of fundamental frequency apparent power.
        /// </summary>
        public static Guid SFund { get; } = new("672d0316-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// Apparent power coincident with active power demand.
        /// </summary>
        public static Guid SCoPDemand { get; } = new("672d0317-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// Apparent power coincident with reactive power demand.
        /// </summary>
        public static Guid SCoQDemand { get; } = new("672d0318-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// True power factor - IEEE arithmetic calculations.
        /// </summary>
        public static Guid PFArith { get; } = new("1c39fb00-a6aa-11d4-a4b3-444553540000");

        /// <summary>
        /// Displacement factor - IEEE arithmetic calculations.
        /// </summary>
        public static Guid DFArith { get; } = new("1c39fb01-a6aa-11d4-a4b3-444553540000");

        /// <summary>
        /// Value of apparent power - IEEE arithmetic calculations.
        /// </summary>
        public static Guid SArith { get; } = new("1c39fb02-a6aa-11d4-a4b3-444553540000");

        /// <summary>
        /// Value of fundamental frequency apparent power - IEEE arithmetic calculations.
        /// </summary>
        public static Guid SArithFund { get; } = new("1c39fb03-a6aa-11d4-a4b3-444553540000");

        /// <summary>
        /// Peak apparent power demand.
        /// </summary>
        public static Guid SPeakDemand { get; } = new("72e82a43-336c-11d5-a4b3-444553540000");

        /// <summary>
        /// Peak reactive power demand.
        /// </summary>
        public static Guid QPeakDemand { get; } = new("72e82a42-336c-11d5-a4b3-444553540000");

        /// <summary>
        /// Peak active power demand.
        /// </summary>
        public static Guid PPeakDemand { get; } = new("72e82a41-336c-11d5-a4b3-444553540000");

        /// <summary>
        /// Net harmonic active power.
        /// </summary>
        public static Guid PHarmonic { get; } = new("b82b5c80-55c7-11d5-a4b3-444553540000");

        /// <summary>
        /// Arithmetic sum harmonic active power.
        /// </summary>
        public static Guid PHarmonicUnsigned { get; } = new("b82b5c81-55c7-11d5-a4b3-444553540000");

        /// <summary>
        /// Value of fundamental frequency real power.
        /// </summary>
        public static Guid PFund { get; } = new("1cdda475-1ebb-42d8-8087-d01b0b5cfa97");

        /// <summary>
        /// Value of active power integrated over time (Energy - watt-hours).
        /// </summary>
        public static Guid PIntg { get; } = new("07ef68a6-9ff5-11d2-b30b-006008b37183");

        /// <summary>
        /// Value of active power integrated over time (Energy - watt-hours) in the positive direction (toward load).
        /// </summary>
        public static Guid PIntgPos { get; } = new("07ef68a7-9ff5-11d2-b30b-006008b37183");

        /// <summary>
        /// Value of active fundamental frequency power integrated over time
        /// (Energy - watt-hours) in the positive direction (toward load).
        /// </summary>
        public static Guid PIntgPosFund { get; } = new("672d0300-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// Value of active power integrated over time (Energy - watt-hours) in the negative direction (away from load).
        /// </summary>
        public static Guid PIntgNeg { get; } = new("07ef68a8-9ff5-11d2-b30b-006008b37183");

        /// <summary>
        /// Value of active fundamental frequency power integrated over time
        /// (Energy - watt-hours) in the negative direction (away from load).
        /// </summary>
        public static Guid PIntgNegFund { get; } = new("672d0301-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// Value of reactive power integrated over time (var-hours).
        /// </summary>
        public static Guid QIntg { get; } = new("07ef68a9-9ff5-11d2-b30b-006008b37183");

        /// <summary>
        /// Value of reactive power integrated over time (Energy - watt-hours) in the positive direction (toward load).
        /// </summary>
        public static Guid QIntgPos { get; } = new("07ef68aa-9ff5-11d2-b30b-006008b37183");

        /// <summary>
        /// Value of fundamental frequency reactive power integrated over time
        /// (Energy - watt-hours) in the positive direction (toward load).
        /// </summary>
        public static Guid QIntgPosFund { get; } = new("672d0303-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// Value of fundamental frequency reactive power integrated over time
        /// (Energy - watt-hours) in the negative direction (away from load).
        /// </summary>
        public static Guid QIntgNegFund { get; } = new("672d0304-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// Value of reactive power integrated over time (Energy - watt-hours) in the negative direction (away from load).
        /// </summary>
        public static Guid QIntgNeg { get; } = new("07ef68ab-9ff5-11d2-b30b-006008b37183");

        /// <summary>
        /// Value of apparent power integrated over time (VA-hours).
        /// </summary>
        public static Guid SIntg { get; } = new("07ef68ac-9ff5-11d2-b30b-006008b37183");

        /// <summary>
        /// Value of fundamental frequency apparent power integrated over time (VA-hours).
        /// </summary>
        public static Guid SIntgFund { get; } = new("672d0313-7810-11d4-a4b3-444553540000");

        /// <summary>
        /// Value of active power integrated over time (Energy - watt-hours).
        /// </summary>
        public static Guid PIVLIntg { get; } = new("f098a9a0-3ee4-11d5-a4b3-444553540000");

        /// <summary>
        /// Value of active power integrated over time (Energy - watt-hours) in the positive direction (toward load).
        /// </summary>
        public static Guid PIVLIntgPos { get; } = new("f098a9a1-3ee4-11d5-a4b3-444553540000");

        /// <summary>
        /// Value of active fundamental frequency power integrated over time
        /// (Energy - watt-hours) in the positive direction (toward load).
        /// </summary>
        public static Guid PIVLIntgPosFund { get; } = new("f098a9a2-3ee4-11d5-a4b3-444553540000");

        /// <summary>
        /// Value of active power integrated over time (Energy - watt-hours) in the negative direction (away from load).
        /// </summary>
        public static Guid PIVLIntgNeg { get; } = new("f098a9a3-3ee4-11d5-a4b3-444553540000");

        /// <summary>
        /// Value of active fundamental frequency power integrated over time
        /// (Energy - watt-hours) in the negative direction (away from load).
        /// </summary>
        public static Guid PIVLIntgNegFund { get; } = new("f098a9a4-3ee4-11d5-a4b3-444553540000");

        /// <summary>
        /// Value of reactive power integrated over time (var-hours).
        /// </summary>
        public static Guid QIVLIntg { get; } = new("f098a9a5-3ee4-11d5-a4b3-444553540000");

        /// <summary>
        /// Value of reactive power integrated over time (Energy - watt-hours) in the positive direction (toward load).
        /// </summary>
        public static Guid QIVLIntgPos { get; } = new("f098a9a6-3ee4-11d5-a4b3-444553540000");

        /// <summary>
        /// Value of fundamental frequency reactive power integrated over time
        /// (Energy - watt-hours) in the positive direction (toward load).
        /// </summary>
        public static Guid QIVLIntgPosFund { get; } = new("f098a9a7-3ee4-11d5-a4b3-444553540000");

        /// <summary>
        /// Value of fundamental frequency reactive power integrated over time
        /// (Energy - watt-hours) in the negative direction (away from load).
        /// </summary>
        public static Guid QIVLIntgNegFund { get; } = new("f098a9a8-3ee4-11d5-a4b3-444553540000");

        /// <summary>
        /// Value of reactive power integrated over time (Energy - watt-hours) in the negative direction (away from load).
        /// </summary>
        public static Guid QIVLIntgNeg { get; } = new("f098a9a9-3ee4-11d5-a4b3-444553540000");

        /// <summary>
        /// Value of apparent power integrated over time (VA-hours).
        /// </summary>
        public static Guid SIVLIntg { get; } = new("f098a9aa-3ee4-11d5-a4b3-444553540000");

        /// <summary>
        /// Value of fundamental frequency apparent power integrated over time (VA-hours).
        /// </summary>
        public static Guid SIVLIntgFund { get; } = new("f098a9ab-3ee4-11d5-a4b3-444553540000");

        /// <summary>
        /// D axis components.
        /// </summary>
        public static Guid DAxisField { get; } = new("d347ba65-e34c-11d4-82d9-00e09872a094");

        /// <summary>
        /// Q axis components.
        /// </summary>
        public static Guid QAxis { get; } = new("d347ba64-e34c-11d4-82d9-00e09872a094");

        /// <summary>
        /// Rotational position.
        /// </summary>
        public static Guid Rotational { get; } = new("d347ba62-e34c-11d4-82d9-00e09872a094");

        /// <summary>
        /// D axis components.
        /// </summary>
        public static Guid DAxis { get; } = new("d347ba63-e34c-11d4-82d9-00e09872a094");

        /// <summary>
        /// Linear position.
        /// </summary>
        public static Guid Linear { get; } = new("d347ba61-e34c-11d4-82d9-00e09872a094");

        /// <summary>
        /// Transfer function.
        /// </summary>
        public static Guid TransferFunc { get; } = new("5202bd07-245c-11d5-a4b3-444553540000");

        /// <summary>
        /// Status data.
        /// </summary>
        public static Guid Status { get; } = new("b82b5c83-55c7-11d5-a4b3-444553540000");

        /// <summary>
        /// Gets information about the quantity
        /// characteristic identified by the given ID.
        /// </summary>
        /// <param name="quantityCharacteristicID">The identifier for the quantity characteristic.</param>
        /// <returns>Inforamtion about the quantity characteristic.</returns>
        public static Identifier? GetInfo(Guid quantityCharacteristicID) => 
            QuantityCharacteristicLookup.TryGetValue(quantityCharacteristicID, out Identifier? identifier) ? identifier : null;

        /// <summary>
        /// Returns the name of the given quantity characteristic.
        /// </summary>
        /// <param name="quantityCharacteristicID">The GUID tag which identifies the quantity characteristic.</param>
        /// <returns>The name of the given quantity characteristic.</returns>
        public static string? ToName(Guid quantityCharacteristicID) =>
            GetInfo(quantityCharacteristicID)?.Name;

        /// <summary>
        /// Returns a string representation of the given quantity characteristic.
        /// </summary>
        /// <param name="quantityCharacteristicID">The GUID tag which identifies the quantity characteristic.</param>
        /// <returns>The name of the given quantity characteristic.</returns>
        public static string? ToString(Guid quantityCharacteristicID) =>
            GetInfo(quantityCharacteristicID)?.Description;

        private static Dictionary<Guid, Identifier> QuantityCharacteristicLookup
        {
            get
            {
                Tag? quantityCharacteristicTag = Tag.GetTag(SeriesDefinition.QuantityCharacteristicIDTag);

                if (s_quantityCharacteristicTag != quantityCharacteristicTag)
                {
                    s_quantityCharacteristicTag = quantityCharacteristicTag;
                    s_quantityCharacteristicLookup = quantityCharacteristicTag?.ValidIdentifiers.ToDictionary(id => Guid.Parse(id.Value));
                }

                return s_quantityCharacteristicLookup ?? new Dictionary<Guid, Identifier>();
            }
        }

        private static Tag? s_quantityCharacteristicTag;
        private static Dictionary<Guid, Identifier>? s_quantityCharacteristicLookup;
    }
}
