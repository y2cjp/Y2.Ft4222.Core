// MIT License
//
// Copyright (c) Y2 Corporation

using System;

namespace Y2.Ft4222.Core
{
    /// <summary>
    /// I2C Master Flag
    /// </summary>
    [Flags]
    public enum I2cMasterFlags
    {
        /// <summary>
        /// No specific flag
        /// </summary>
        None = Iot.Device.Ft4222.I2cMasterFlag.None,

        /// <summary>
        /// Send start
        /// </summary>
        Start = Iot.Device.Ft4222.I2cMasterFlag.Start,

        /// <summary>
        /// Repeated start
        /// </summary>
        RepeatedStart = Iot.Device.Ft4222.I2cMasterFlag.RepeatedStart,

        /// <summary>
        /// Send stop
        /// </summary>
        Stop = Iot.Device.Ft4222.I2cMasterFlag.Stop,

        /// <summary>
        /// Start condition followed by a stop condition
        /// </summary>
        StartAndStop = Iot.Device.Ft4222.I2cMasterFlag.StartAndStop,
    }
}
