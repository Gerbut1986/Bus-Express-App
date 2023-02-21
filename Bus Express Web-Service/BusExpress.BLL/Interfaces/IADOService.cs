namespace BusExpress.BLL.Interfaces
{
    public interface IADOService
    {
        string Create(IModel model, string conn);
        string Update(IModel model, string conn);
        string Delete(int id, string conn);
    }
}
