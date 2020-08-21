// MIT License
//
// Copyright (c) Y2 Corporation

using System;
using System.IO;
using System.Runtime.InteropServices;
using Iot.Device.Ft4222;

namespace Y2.Ft4222.Core
{
    /// <summary>
    /// FT4222 I2C Master Device
    /// </summary>
    public class Ft4222I2cMaster : Ft4222I2cEx, IFt4222I2cMaster
    {
        private readonly Ft4222I2cConnectionSettings _settings;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Ft4222I2cMaster"/> class.
        /// </summary>
        /// <param name="settings">I2C Connection Settings</param>
        public Ft4222I2cMaster(Ft4222I2cConnectionSettings settings)
            : base(settings)
        {
            _settings = settings;
        }

        /// <inheritdoc/>
        public void Read(int slaveAddress, Span<byte> buffer)
        {
            var ftStatus = FtFunction.FT4222_I2CMaster_Read(FtHandle, (ushort)slaveAddress, in MemoryMarshal.GetReference(buffer), (ushort)buffer.Length, out _);
            if (ftStatus != FtStatus.Ok)
            {
                throw new IOException($"{nameof(Read)} failed to read, error: {ftStatus}");
            }
        }

        /// <inheritdoc/>
        public void Write(int slaveAddress, ReadOnlySpan<byte> buffer)
        {
            var ftStatus = FtFunction.FT4222_I2CMaster_Write(FtHandle, (ushort)slaveAddress, in MemoryMarshal.GetReference(buffer), (ushort)buffer.Length, out _);
            if (ftStatus != FtStatus.Ok)
            {
                throw new IOException($"{nameof(Write)} failed to write, error: {ftStatus}");
            }
        }

        /// <inheritdoc/>
        public void ReadEx(I2cMasterFlags flags, Span<byte> buffer)
        {
            ReadEx(_settings.DeviceAddress, flags, buffer);
        }

        /// <inheritdoc/>
        public void ReadEx(int slaveAddress, I2cMasterFlags flags, Span<byte> buffer)
        {
            var result = FtFunction.FT4222_I2CMaster_ReadEx(FtHandle, (ushort)slaveAddress, (byte)flags, in MemoryMarshal.GetReference(buffer), (ushort)buffer.Length, out _);
            if (result != FtStatus.Ok)
                throw new IOException();
        }

        /// <inheritdoc/>
        public void WriteEx(I2cMasterFlags flags, ReadOnlySpan<byte> buffer)
        {
            WriteEx(_settings.DeviceAddress, flags, buffer);
        }

        /// <inheritdoc/>
        public void WriteEx(int slaveAddress, I2cMasterFlags flags, ReadOnlySpan<byte> buffer)
        {
            var ftStatus = FtFunction.FT4222_I2CMaster_WriteEx(FtHandle, (ushort)slaveAddress, (byte)flags, in MemoryMarshal.GetReference(buffer), (ushort)buffer.Length, out _);
            if (ftStatus != FtStatus.Ok)
            {
                throw new IOException($"{nameof(WriteEx)} failed to write, error: {ftStatus}");
            }
        }

        /// <inheritdoc/>
        public void Reset()
        {
            var ftStatus = FtFunction.FT4222_I2CMaster_Reset(FtHandle);
            if (ftStatus != FtStatus.Ok)
            {
                throw new IOException($"Failed to reset: {ftStatus}");
            }
        }

        /// <inheritdoc/>
        public byte GetStatus()
        {
            var ftStatus = FtFunction.FT4222_I2CMaster_GetStatus(FtHandle, out var controllerStatus);
            if (ftStatus != FtStatus.Ok)
            {
                throw new IOException($"Failed to get status: {ftStatus}");
            }

            return controllerStatus;
        }

        /// <inheritdoc/>
        public FtVersion GetVersionValues()
        {
            var ftStatus = FtFunction.FT4222_GetVersion(FtHandle, out var ftVersion);
            if (ftStatus != FtStatus.Ok)
            {
                throw new IOException($"Can't find versions of chipset and FT4222, status: {ftStatus}");
            }

            return ftVersion;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            Close();
            if (disposing)
                FtHandle.Dispose();

            disposed = true;
            base.Dispose(disposing);
        }

        private void Close()
        {
            FtFunction.FT4222_UnInitialize(FtHandle);
            FtFunction.FT_Close(FtHandle);
        }
    }
}
