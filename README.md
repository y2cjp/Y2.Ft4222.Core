# USB-I2C変換ボード用ドライバ

FTDI社の [FT4222H](https://www.ftdichip.com/Products/ICs/FT4222H.html) を介して、USBからI2Cデバイスを制御する為のドライバです。  
Windows・Linux・MacOSで使用できます。 

## 対応ハードウェア

* [USB-I2C変換ボード（絶縁デジタル入出力付） DIO-8/4RE-UBC](https://www.y2c.co.jp/i2c-r/dio-8-4re-ubc/)
* その他、FT4222Hを搭載したハードウェア（モード0または3でI2Cマスターとして使用できるもの） 

## セットアップ方法

Windows 64bit

1. [FT4222Hの製品ページ](https://www.ftdichip.com/Products/ICs/FT4222H.html) から「LibFT4222 Windows Library」をダウンロードして解凍します。  
2. ```LibFT4222-vx.x.x\imports\LibFT4222\lib\amd64\LibFT4222-64.dll```をアプリケーションの実行可能ファイルと同じディレクトリにコピーします。  
3. ```LibFT4222-64.dll```を```LibFT4222.dll```に名前を変更します。  

（レジストリ登録やPATHで登録しても可）

Windows 32bit

1. [FT4222Hの製品ページ](https://www.ftdichip.com/Products/ICs/FT4222H.html) から「LibFT4222 Windows Library and Examples」をダウンロードして解凍します。  
2. ```LibFT4222-vx.x.x\imports\LibFT4222\lib\i386\LibFT4222.dll```をアプリケーションの実行可能ファイルと同じディレクトリにコピーします。  

（レジストリ登録やPATHで登録しても可）

Linux

1. [FT4222Hの製品ページ](https://www.ftdichip.com/Products/ICs/FT4222H.html) から「LibFT4222 Linux Library and Examples」をダウンロードして解凍します。  
2. 管理者権限で```install4222.sh```を実行します。  

MAC OSX

1. [D2XX Driversのダウンロード](https://www.ftdichip.com/Drivers/D2XX.htm) から```libftd2xx.dylib```をダウンロードして解凍します。  
2. [FT4222Hの製品ページ](https://www.ftdichip.com/Products/ICs/FT4222H.html) から「LibFT4222 MAC OSX Library and Examples」をダウンロードして解凍します。  
3. ターミナルで以下のコマンドを実行します。  

```bash
# /usr/local/libディレクトリがなければ作成
$ sudo mkdir /usr/local/lib
# /usr/local/includeディレクトリがなければ作成
$ sudo mkdir /usr/local/include
# libftd2xx.1.4.16.dylibが解凍されたパスに移動してから
# ファイルをコピー
$ sudo cp libftd2xx.1.4.16.dylib /usr/local/lib/libftd2xx.1.4.16.dylib
# シンボリックリンクを作成
$ sudo ln -sf /usr/local/lib/libftd2xx.1.4.16.dylib /usr/local/lib/libftd2xx.dylib
# libft4222.1.4.4.14.dylibが解凍されたパスに移動してから
# ファイルをコピー
$ sudo cp libft4222.1.4.4.14.dylib /usr/local/lib/libft4222.1.4.4.14.dylib
# シンボリックリンクを作成
$ sudo ln -sf /usr/local/lib/libft4222.1.4.4.14.dylib /usr/local/lib/libft4222.dylib
# ファイルをコピー
$ sudo cp boost_libs/libboost_system.dylib /usr/local/lib/libboost_system.dylib
$ sudo cp boost_libs/libboost_thread-mt.dylib /usr/local/lib/libboost_thread-mt.dylib
```

## 使用方法

```csharp
// スレーブアドレス 0x26のデバイスにアクセス。I2Cクロック周波数は400kHz。
var i2c = new Ft4222I2cMaster(new Ft4222I2cConnectionSettings(0, 0x26, 400));

// 読み出し
Span<byte> buffer = stackalloc byte[2];
i2c.Read(buffer);

// 書き込み
byte[] writeBuffer = { 0x12, 0x34 };
i2c.Write(writeBuffer);

// I2cMasterFlagを指定しての読み出し
i2c.ReadEx(I2cMasterFlag.RepeatedStart, buffer);

// I2cMasterFlagを指定しての書き込み
i2c.WriteEx(I2cMasterFlag.RepeatedStart, writeBuffer);

// Ft4222I2cMasterインスタンス生成時と異なるデバイス（スレーブアドレス 0x23）から読み出し
i2c.Read(0x23, buffer);

// Ft4222I2cMasterインスタンス生成時と異なるデバイス（スレーブアドレス 0x23）に書き込み
i2c.Write(0x23, writeBuffer);
```

MicrosoftのIot.DeviceのI2Cデバイスに渡して使用する事もできます。

```csharp
var i2c = new Ft4222I2cMaster(new Ft4222I2cConnectionSettings(0, 0x26, 400));

var bno055Sensor = new Bno055Sensor(i2c);

Console.WriteLine($"Id: {bno055Sensor.Info.ChipId}, AccId: {bno055Sensor.Info.AcceleratorId}, GyroId: {bno055Sensor.Info.GyroscopeId}, MagId: {bno055Sensor.Info.MagnetometerId}");
Console.WriteLine($"Firmware version: {bno055Sensor.Info.FirmwareVersion}, Bootloader: {bno055Sensor.Info.BootloaderVersion}");
Console.WriteLine($"Temperature source: {bno055Sensor.TemperatureSource}, Operation mode: {bno055Sensor.OperationMode}, Units: {bno055Sensor.Units}");
Console.WriteLine($"Powermode: {bno055Sensor.PowerMode}");
```

## 関連プロジェクト

* [Y2.Dio84ReUbc.Core](https://github.com/y2cjp/Y2.Dio84ReUbc.Core)  
  [USB-I2C変換ボード（絶縁デジタル入出力付） DIO-8/4RE-UBC](https://www.y2c.co.jp/i2c-r/dio-8-4re-ubc/) 用ライブラリ

* [DIO-8-4RE-UBC-ExampleCs](https://github.com/y2cjp/DIO-8-4RE-UBC-ExampleCs)  
  [USB-I2C変換ボード（絶縁デジタル入出力付） DIO-8/4RE-UBC](https://www.y2c.co.jp/i2c-r/dio-8-4re-ubc/) 用サンプル

* [.NET Core IoT Libraries](https://github.com/dotnet/iot)
