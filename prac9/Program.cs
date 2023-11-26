using System;

// Абстрактный класс "Носитель информации"
public abstract class Storage
{
    protected string name;
    protected string model;

    public Storage(string name, string model)
    {
        this.name = name;
        this.model = model;
    }

    public abstract double GetMemory();

    public abstract void CopyData(double dataSize);

    public abstract double GetFreeMemory();

    public abstract void DisplayInfo();
}

// Класс "Flash-память"
public class Flash : Storage
{
    private double usbSpeed;
    private double memorySize;

    public Flash(string name, string model, double usbSpeed, double memorySize)
        : base(name, model)
    {
        this.usbSpeed = usbSpeed;
        this.memorySize = memorySize;
    }

    public override double GetMemory()
    {
        return memorySize;
    }

    public override void CopyData(double dataSize)
    {
        Console.WriteLine($"Копирование данных на Flash-память. Размер данных: {dataSize} Гб");
    }

    public override double GetFreeMemory()
    {
        // Предполагаем, что половина памяти занята
        return memorySize / 2;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Flash-память: {name}, Модель: {model}, Скорость USB: {usbSpeed} Гбит/c, Объем памяти: {memorySize} Гб");
    }
}

// Класс "DVD-диск"
public class DVD : Storage
{
    private double readWriteSpeed;
    private bool isDoubleLayered;

    public DVD(string name, string model, double readWriteSpeed, bool isDoubleLayered)
        : base(name, model)
    {
        this.readWriteSpeed = readWriteSpeed;
        this.isDoubleLayered = isDoubleLayered;
    }

    public override double GetMemory()
    {
        return isDoubleLayered ? 9 : 4.7;
    }

    public override void CopyData(double dataSize)
    {
        Console.WriteLine($"Запись данных на DVD-диск. Размер данных: {dataSize} Гб");
    }

    public override double GetFreeMemory()
    {
        // Предполагаем, что DVD-диск всегда свободен перед записью
        return isDoubleLayered ? 9 : 4.7;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"DVD-диск: {name}, Модель: {model}, Скорость чтения/записи: {readWriteSpeed} Гбит/c, Тип: {(isDoubleLayered ? "Двусторонний" : "Односторонний")}");
    }
}

// Класс "Съемный HDD"
public class HDD : Storage
{
    private double usbSpeed;
    private int partitionsCount;
    private double partitionSize;

    public HDD(string name, string model, double usbSpeed, int partitionsCount, double partitionSize)
        : base(name, model)
    {
        this.usbSpeed = usbSpeed;
        this.partitionsCount = partitionsCount;
        this.partitionSize = partitionSize;
    }

    public override double GetMemory()
    {
        return partitionsCount * partitionSize;
    }

    public override void CopyData(double dataSize)
    {
        Console.WriteLine($"Копирование данных на съемный HDD. Размер данных: {dataSize} Гб");
    }

    public override double GetFreeMemory()
    {
        // Предполагаем, что половина памяти занята
        return partitionsCount * partitionSize / 2;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Съемный HDD: {name}, Модель: {model}, Скорость USB: {usbSpeed} Гбит/c, Количество разделов: {partitionsCount}, Объем разделов: {partitionSize} Гб");
    }
}

class Program
{
    static void Main()
    {
        // Создаем массив объектов базового класса Storage
        Storage[] devices = new Storage[]
        {
            new Flash("FlashDrive1", "Kingston", 3.0, 64),
            new DVD("DVD1", "Sony", 8.0, true),
            new HDD("HDD1", "Seagate", 2.0, 2, 500)
        };

        // Вычисляем общий объем памяти всех устройств
        double totalMemory = 0;
        foreach (var device in devices)
        {
            totalMemory += device.GetMemory();
        }

        Console.WriteLine($"Общий объем памяти всех устройств: {totalMemory} Гб");

        // Симулируем копирование данных и вычисляем время
        double dataSize = 565; // Размер данных в Гб
        foreach (var device in devices)
        {
            device.CopyData(dataSize);
        }

        // Вычисляем необходимое количество носителей информации для переноса данных
        double dataSizePerDevice = 780; // Размер файла в Мб
        int neededDevices = (int)Math.Ceiling(dataSize / (dataSizePerDevice / 1024)); // Преобразуем Мб в Гб
        Console.WriteLine($"Необходимое количество устройств: {neededDevices}");

        // Выводим информацию о каждом устройстве
        foreach (var device in devices)
        {
            device.DisplayInfo();
            Console.WriteLine($"Свободная память: {device.GetFreeMemory()} Гб\n");
        }
    }
}
