# USB-I2C変換ボード用ドライバ

FTDI社の [FT4222H](https://ftdichip.com/products/ft4222h/) を介して、USBからI2Cデバイスを制御する為のドライバです。  
Windows・Linux・Mac で使用できます。 

## 対応ハードウェア

* [USB-I2C変換ボード（絶縁デジタル入出力付） DIO-8/4RE-UBC](https://www.y2c.co.jp/i2c-r/dio-8-4re-ubc/)
* その他、FT4222Hを搭載したハードウェア（モード0または3でI2Cマスターとして使用できるもの） 

## セットアップ

1. Visual Studioで、プロジェクトを新規作成するか既存のプロジェクトを開きます。  
（.NETに対応したプロジェクト）

2. NuGetパッケージの「Y2.Ft4222.Core」をインストールします。  

    * ソリューションエクスプローラーからプロジェクトを右クリックして「NuGetパッケージの管理」をクリックします。  
    * タブを「参照」にして、パッケージソースを「nuget.org」にします。  
    * 検索窓に「Y2.Ft」を入力すると「Y2.Ft4222.Core」が見つかりますので選択します。  
    * バージョンは「最新の安定版」にして「インストール」をクリックします。

3. FTDI社のライブラリ（デバイスドライバ含む）を以下の各OSの手順にしたがってインストールします。

    Windows 64bit

    * [FT4222Hの製品ページ](https://ftdichip.com/products/ft4222h/) の「Downloads」から「LibFT4222 Windows Library」をダウンロードして解凍します。  
    * ```LibFT4222-vx.x.x\imports\LibFT4222\lib\amd64\LibFT4222-64.dll``` をアプリケーションの実行可能ファイルと同じディレクトリにコピーします。  
    * ```LibFT4222-64.dll``` を ```LibFT4222.dll``` に名前を変更します。  

    （レジストリ登録やPATHで登録しても可）

    Windows 32bit

    * [FT4222Hの製品ページ](https://ftdichip.com/products/ft4222h/) の「Downloads」から「LibFT4222 Windows Library and Examples」をダウンロードして解凍します。  
    * ```LibFT4222-vx.x.x\imports\LibFT4222\lib\i386\LibFT4222.dll``` をアプリケーションの実行可能ファイルと同じディレクトリにコピーします。  

    （レジストリ登録やPATHで登録しても可）

    Linux

    * [FT4222Hの製品ページ](https://ftdichip.com/products/ft4222h/) の「Downloads」から「LibFT4222 Linux Library and Examples」をダウンロードして解凍します。  
    * 管理者権限で ```install4222.sh``` を実行します。  

    Mac

    * [D2XX Driversのダウンロード](https://ftdichip.com/drivers/d2xx-drivers/) から「D2XX Drives」をダウンロードしてインストールします。  
    * [FT4222Hの製品ページ](https://ftdichip.com/products/ft4222h/) の「Downloads」から「LibFT4222 MAC OSX Library and Examples」をダウンロードして解凍します。  

## 使用方法

```csharp
// スレーブアドレス 0x26のデバイスにアクセスできるようにインスタンスを生成。I2Cクロック周波数は400kHz。
var i2c = new Ft4222I2cMaster(new Ft4222I2cConnectionSettings(0, 0x26, 400));

// 読み出し
Span<byte> buffer = stackalloc byte[2];
i2c.Read(buffer);

// 書き込み
byte[] writeBuffer = { 0x12, 0x34 };
i2c.Write(writeBuffer);

// I2cMasterFlagsを指定しての読み出し
i2c.ReadEx(I2cMasterFlags.RepeatedStart, buffer);

// I2cMasterFlagsを指定しての書き込み
i2c.WriteEx(I2cMasterFlags.RepeatedStart, writeBuffer);

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

* [.NET IoT Libraries](https://github.com/dotnet/iot)
