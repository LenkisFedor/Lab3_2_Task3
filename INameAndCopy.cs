namespace Lab3_2_Task3
{
    public interface INameAndCopy
    {
        string Name { get; set; }
        object DeepCopy();
    }
}