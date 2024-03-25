namespace fgeek.Services
{
    public static class ByteConverterService
    {
        public static IEnumerable<int> ToIntCollection(IEnumerable<byte> src)
        {
            var rs = new int[src.Count() / sizeof(int)];
            Buffer.BlockCopy(src.ToArray(), 0, rs, 0, src.Count());

            return rs;
        }

        public static IEnumerable<byte> ToByteCollection(IEnumerable<int> src)
        {
            var rs = new List<byte>();
            foreach (var item in src)
            {
                rs.InsertRange(rs.Count, BitConverter.GetBytes(item));
            }

            return rs;
        }
    }
}
