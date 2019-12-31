namespace DC
{
    public static class objectExtensions
    {
        public static int ToHashId<T>(this T obj)
        {
            return obj.ToString().GetExtHashCode();
        }
    }
}