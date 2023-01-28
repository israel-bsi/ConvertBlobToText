using FirebirdSql.Data.FirebirdClient;

namespace ConvertBlobToText;

internal class Program
{
    public static string ConnectionString = "";
    static void Main()
    {
        try
        {
            GetBlob();
            Console.WriteLine("Sucesso");
            Console.Read();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    private static void GetBlob()
    {
        const string sqlCommand = "";
        using var conn = new FbConnection(ConnectionString);
        using var cmd = new FbCommand(sqlCommand, conn);
        cmd.Connection.Open();
        var reader = cmd.ExecuteReader();
        var ms = new MemoryStream();
        var text = "";
        while (reader.Read())
        {
            var blob = new byte[reader.GetBytes(0, 0, null, 0, int.MaxValue)];
            try
            {
                reader.GetBytes(0, 0, blob, 0, blob.Length);
            }
            catch
            {
                text = "";
            }
            ms = new MemoryStream(blob);
        }

        var streamReader = new StreamReader(ms);
        while (streamReader.Peek() != -1)
        {
            text = text + "\r\n" + streamReader.ReadLine();
        }

        const string pathFinal = "";
        using var sw = new StreamWriter(pathFinal, true);
        sw.Write(text);
    }
}