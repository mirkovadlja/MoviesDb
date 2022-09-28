namespace MoviesDb.Model.Common.TMBD
{
    public interface ITmbdCrew
    {
        int Id{ get; set; }
        string Name { get; set; }
        string Job { get; set; }
    }
}
