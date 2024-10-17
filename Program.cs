using System;

public class Program
{
    public static void Main(string[] args)
    {
        Kemampuan perbaikan = new Perbaikan(2);
        Kemampuan seranganListrik = new SeranganListrik(3);

        Robot robot1 = new RobotBiasa("Robot A", 120, 60, 30, perbaikan);

        Robot robot2 = new RobotBiasa("Robot B", 90, 50, 25, seranganListrik);

        BosRobot bosRobot = new BosRobot("Bos Robot", 250, 80, 0, 100);

        Console.WriteLine("=== Sebelum Pertempuran ===");
        robot1.CetakInformasi();
        robot2.CetakInformasi();
        bosRobot.CetakInformasi();

        Console.WriteLine("\n=== Pertempuran Dimulai ===");
        robot1.Serang(bosRobot); 
        bosRobot.Diserang(robot1); 

        robot2.GunakanKemampuan(seranganListrik); 
        robot2.Serang(bosRobot); 
        bosRobot.Diserang(robot2); 

        robot1.GunakanKemampuan(perbaikan); 

        Console.WriteLine("\n=== Setelah Pertempuran ===");
        robot1.CetakInformasi();
        robot2.CetakInformasi();
        bosRobot.CetakInformasi();

        if (bosRobot.Energi <= 0)
        {
            bosRobot.Mati();
        }
    }
}


public interface Kemampuan
{
    void GunakanKemampuan(Robot robot);
    int GetCooldown();
}

public abstract class Robot
{
    protected string nama;
    protected int energi;
    protected int armor;
    protected int serangan;

    public Robot(string nama, int energi, int armor, int serangan)
    {
        this.nama = nama;
        this.energi = energi;
        this.armor = armor;
        this.serangan = serangan;
    }

    public int Energi
    {
        get { return energi; }
        set { energi = value; }
    }

    public int Armor
    {
        get { return armor; }
        set { armor = value; }
    }

    public int Serangan
    {
        get { return serangan; }
        set { serangan = value; }
    }

    public abstract void Serang(Robot target);

    public abstract void GunakanKemampuan(Kemampuan kemampuan);

    public void CetakInformasi()
    {
        Console.WriteLine("Nama: " + nama);
        Console.WriteLine("Energi: " + energi);
        Console.WriteLine("Armor: " + armor);
        Console.WriteLine("Serangan: " + serangan);
    }
}

public class BosRobot : Robot
{
    private int pertahanan;

    public BosRobot(string nama, int energi, int armor, int serangan, int pertahanan)
        : base(nama, energi, armor, serangan)
    {
        this.pertahanan = pertahanan;
    }

    public override void Serang(Robot target)
    {

    }

    public override void GunakanKemampuan(Kemampuan kemampuan)
    {

    }

    public void Diserang(Robot penyerang)
    {
        energi -= penyerang.Serangan; 
        if (energi <= 0)
        {
            Mati();
        }
    }

    public void Mati()
    {
        Console.WriteLine("Bos robot telah mati!");
    }
}

public class RobotBiasa : Robot
{
    private Kemampuan kemampuan;

    public RobotBiasa(string nama, int energi, int armor, int serangan, Kemampuan kemampuan)
        : base(nama, energi, armor, serangan)
    {
        this.kemampuan = kemampuan;
    }

    public override void Serang(Robot target)
    {
        target.Energi -= serangan; 
    }

    public override void GunakanKemampuan(Kemampuan kemampuan)
    {
        kemampuan.GunakanKemampuan(this);
    }
}

public class Perbaikan : Kemampuan
{
    private int cooldown;

    public Perbaikan(int cooldown)
    {
        this.cooldown = cooldown;
    }

    public void GunakanKemampuan(Robot robot)
    {
        robot.Energi += 10; 
        Console.WriteLine("Robot sedang menjalani perbaikan!");
    }

    public int GetCooldown()
    {
        return cooldown;
    }
}

public class SeranganListrik : Kemampuan
{
    private int cooldown;

    public SeranganListrik(int cooldown)
    {
        this.cooldown = cooldown;
    }

    public void GunakanKemampuan(Robot robot)
    {
        robot.Energi -= 5;
        Console.WriteLine("Energi robot berkurang akibat serangan listrik!");
    }

    public int GetCooldown()
    {
        return cooldown;
    }
}
