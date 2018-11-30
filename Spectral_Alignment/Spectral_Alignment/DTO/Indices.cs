namespace Spectral_Alignment.DTO
{
    /// <summary>
    /// This Data Transfer Object will help in keeping track of Grid indices.
    /// </summary>
    public class Indices
    {
        public Indices()
        {
        }

        public Indices(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
