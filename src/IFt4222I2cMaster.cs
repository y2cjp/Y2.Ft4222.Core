// MIT License
//
// Copyright (c) Y2 Corporation

using System;
using Iot.Device.Ft4222;

namespace Y2.Ft4222.Core
{
    public interface IFt4222I2cMaster
    {
        /// <summary>The speed of I2C transmission.</summary>
        uint FrequencyKbps { get; }

        /// <summary>Reads data from the I2C device.</summary>
        /// <param name="buffer">The buffer to read the data from the I2C device.</param>
        void Read(Span<byte> buffer);

        /// <summary>Reads data from the I2C device.</summary>
        /// <param name="slaveAddress">The bus address of the I2C device.</param>
        /// <param name="buffer">The buffer to read the data from the I2C device.</param>
        void Read(int slaveAddress, Span<byte> buffer);

        /// <summary>Writes data to the I2C device.</summary>
        /// <param name="buffer">The buffer that contains the data to be written to the I2C device.</param>
        void Write(ReadOnlySpan<byte> buffer);

        /// <summary>Writes data to the I2C device.</summary>
        /// <param name="slaveAddress">The bus address of the I2C device.</param>
        /// <param name="buffer">The buffer that contains the data to be written to the I2C device.</param>
        void Write(int slaveAddress, ReadOnlySpan<byte> buffer);

        /// <summary>Reads data from the I2C device.</summary>
        /// <param name="flags">flags</param>
        /// <param name="buffer">The buffer to read the data from the I2C device.</param>
        void ReadEx(I2cMasterFlag flags, Span<byte> buffer);

        /// <summary>Reads data from the I2C device.</summary>
        /// <param name="slaveAddress">The bus address of the I2C device.</param>
        /// <param name="flags">flags</param>
        /// <param name="buffer">The buffer to read the data from the I2C device.</param>
        void ReadEx(int slaveAddress, I2cMasterFlag flags, Span<byte> buffer);

        /// <summary>Writes data to the I2C device.</summary>
        /// <param name="flags">flags</param>
        /// <param name="buffer">The buffer that contains the data to be written to the I2C device.</param>
        void WriteEx(I2cMasterFlag flags, ReadOnlySpan<byte> buffer);

        /// <summary>Writes data to the I2C device.</summary>
        /// <param name="slaveAddress">The bus address of the I2C device.</param>
        /// <param name="flags">flags</param>
        /// <param name="buffer">The buffer that contains the data to be written to the I2C device.</param>
        void WriteEx(int slaveAddress, I2cMasterFlag flags, ReadOnlySpan<byte> buffer);

        /// <summary>Reset I2C as a master</summary>
        void Reset();

        /// <summary>Get the I2C status as a master</summary>
        /// <returns>controller status</returns>
        byte GetStatus();

        /// <summary>Get the version of the chip and dll</summary>
        /// <returns>version</returns>
        (Version chip, Version dll) GetVersions();

        /// <summary>Get the version of the chip and dll</summary>
        /// <returns>version</returns>
        FtVersion GetVersionValues();
    }
}
