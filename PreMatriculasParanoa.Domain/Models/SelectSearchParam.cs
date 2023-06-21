namespace PreMatriculasParanoa.Domain.Models.Base;

public class SelectSearchParam
{
    public SelectSearchParam()
    {
    }

    public SelectSearchParam(string term)
    {
        Term = term;
    }

    public SelectSearchParam(string term, int size)
    {
        Term = term;
        Size = size;
    }

    public SelectSearchParam(string term, int size, int selected)
    {
        Term = term;
        Size = size;
        Selected = selected;
    }

    public string Term { get; set; }
    public int Size { get; set; }
    public int Selected { get; set; } = 0;
}
