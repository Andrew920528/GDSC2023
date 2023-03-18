using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public class WikipediaReader
{
    WebClient client = new WebClient();
}


public class WikiResult
{
    public WikiQuery query { get; set; }
}

public class WikiQuery
{
    public Dictionary<string, WikiPage> pages { get; set; }
}

public class WikiPage
{
    public string extract { get; set; }
}