// MIT License
//
// Copyright (c) Y2 Corporation

using System;
using Iot.Device.Ft4222;

namespace Y2.Ft4222.Core
{
    public class Ft4222I2cSlaveDevice
    {
        private readonly IFt4222I2cMaster _i2cMaster;
        private readonly int _slaveAddress;

        /// <summary>
        /// Initializes a new instance of the <see cref="Ft4222I2cSlaveDevice"/> class.
        /// </summary>
        /// <param name="i2cMaster">The I2C master device</param>
        /// <param name="slaveAddress">The bus address of the I2C device.</param>
        public Ft4222I2cSlaveDevice(IFt4222I2cMaster i2cMaster, int slaveAddress)
        {
            _i2cMaster = i2cMaster ?? throw new ArgumentNullException(nameof(i2cMaster));
            _slaveAddress = slaveAddress;
        }

        /// <summary>Reads data from the I2C device.</summary>
        /// <param name="buffer">The buffer to read the data from the I2C device.</param>
        public void Read(Span<byte> buffer)
        {
            _i2cMaster.Read(_slaveAddress, buffer);
        }

        /// <summary>Writes data to the I2C device.</summary>
        /// <param name="buffer">The buffer that contains the data to be written to the I2C device.</param>
        public void Write(ReadOnlySpan<byte> buffer)
        {
            _i2cMaster.Write(_slaveAddress, buffer);
        }

        /// <summary>Reads data from the I2C device.</summary>
        /// <param name="flags">flags</param>
        /// <param name="buffer">The buffer to read the data from the I2C device.</param>
        public void ReadEx(I2cMasterFlag flags, Span<byte> buffer)
        {
            _i2cMaster.ReadEx(_slaveAddress, flags, buffer);
        }

        /// <summary>Writes data to the I2C device.</summary>
        /// <param name="flags">flags</param>
        /// <param name="buffer">The buffer that contains the data to be written to the I2C device.</param>
        public void WriteEx(I2cMasterFlag flags, ReadOnlySpan<byte> buffer)
        {
            _i2cMaster.WriteEx(_slaveAddress, flags, buffer);
        }
    }
}
