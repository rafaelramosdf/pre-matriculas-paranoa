namespace PreMatriculasParanoa.Domain.Models.ViewModels;

public class DashboardTotalizadoresViewModel
{
    public int TotalGeralVagas { get; set; }
    public int TotalVagasPreenchidas { get; set; }
    public int TotalVagasLiberadas { get; set; }
    public int TotalVagasDisponiveis => TotalGeralVagas - TotalVagasPreenchidas + TotalVagasLiberadas;
}
