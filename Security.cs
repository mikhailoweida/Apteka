using System.Security.Cryptography;
using System.Text;

namespace Apteka;

public static class Session
{
    public static int UserId;
    public static string Login;
    public static string Role;      // "Администратор", "Фармацевт", "Гость"
}

public static class Security
{
    public static string Hash(string s) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(s)));

    // C R U D per entity; empty string = no access
    public static string Perm(string entity)
    {
        switch (Session.Role)
        {
            case "Администратор":
                return "CRUD";

            case "Фармацевт":
                // Pharmacist: full CRUD on people/orders/prescriptions, read everything else
                if (entity is "Patient" or "Order" or "OrderComposition"
                           or "Prescription" or "PrescriptionComposition"
                           or "OrderPrescription" or "OrderSubstanceRequest"
                           or "OrderComponentRequest")
                    return "CRUD";
                return "R";

            case "Гость":
                // Guest: only read the catalog
                if (entity is "Medicine" or "ReadyMedicine" or "CompoundedMedicine"
                           or "Manufacturer" or "PreparationTechnology" or "City" or "Street")
                    return "R";
                return "";

            default:
                return "";
        }
    }
}

public static class Logger
{
    public static void Write(string action)
    {
        try
        {
            Db.Exec("INSERT INTO ActionLog (login, action) VALUES (@l, @a)",
                    ("@l", Session.Login ?? "-"), ("@a", action));
        }
        catch { }
    }
}
