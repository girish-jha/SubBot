namespace RosettaMovies
{
    sealed class QueryString
    {
        public QueryString(string hash, string action, string language)
        {
            Hash = hash;
            Action = action;
            Language = language;
        }

        public static QueryString CreateForDownload(string hash)
        {
            return new QueryString(hash, "download", "en");
        }

        public static implicit operator string(QueryString queryString)
        {
            return string.Format("?action={0}&hash={1}&language={2}", queryString.Action, queryString.Hash, queryString.Language);
        }
        public string Language { get; private set; }
        public string Action { get; private set; }
        public string Hash { get; private set; }

 
    }
}