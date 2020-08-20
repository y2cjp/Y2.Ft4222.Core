// MIT License
//
// Copyright (c) Y2 Corporation

using System.Device.I2c;

namespace Y2.Ft4222.Core
{
    /// <summary>The connection settings of a device on an I2C bus.</summary>
    public class Ft4222I2cConnectionSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ft4222I2cConnectionSettings" /> class.
        /// </summary>
        /// <param name="busId">The bus ID the I2C device is connected to.</param>
        /// <param name="deviceAddress">The bus address of the I2C device.</param>
        /// <param name="frequencyKbps">The speed of I2C transmission.</param>
        public Ft4222I2cConnectionSettings(int busId, int deviceAddress, uint frequencyKbps)
            : this(new I2cConnectionSettings(busId, deviceAddress), frequencyKbps)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ft4222I2cConnectionSettings" /> class.
        /// </summary>
        /// <param name="i2cConnectionSettings">The I2C connection settings.</param>
        /// <param name="frequencyKbps">The speed of I2C transmission.</param>
        public Ft4222I2cConnectionSettings(I2cConnectionSettings i2cConnectionSettings, uint frequencyKbps)
        {
            I2cConnectionSettings = i2cConnectionSettings;
            FrequencyKbps = frequencyKbps;
        }

        /// <summary>
        /// The connection settings of a device on an I2C bus.
        /// </summary>
        public I2cConnectionSettings I2cConnectionSettings { get; }

        /// <summary>
        /// The bus frequency of the I2C device.
        /// </summary>
        public uint FrequencyKbps { get; }

        /// <summary>
        /// The bus ID the I2C device is connected to.
        /// </summary>
        public int BusId => I2cConnectionSettings.BusId;

        /// <summary>
        /// The bus address of the I2C device.
        /// </summary>
        public int DeviceAddress => I2cConnectionSettings.DeviceAddress;
    }
}
