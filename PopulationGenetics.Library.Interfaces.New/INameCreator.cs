namespace PopulationGenetics.Library.Interfaces
{
    public interface INameCreator
    {
        string CreateName(IAllele first, IAllele second);
    }
}