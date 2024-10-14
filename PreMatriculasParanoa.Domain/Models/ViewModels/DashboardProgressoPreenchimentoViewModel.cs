using System;

namespace PreMatriculasParanoa.Domain.Models.ViewModels;

public class DashboardProgressoPreenchimentoViewModel
{
    public int TotalEscolasCadastradas { get; set; }
    public int TotalEscolasPreenchendo { get; set; }
    public int TotalEscolasNaoPreenchendo => TotalEscolasCadastradas - TotalEscolasPreenchendo;
    public int PercentualEscolasPreenchendo => TotalEscolasCadastradas > 0 && TotalEscolasPreenchendo > 0 
        ? Convert.ToInt32(TotalEscolasPreenchendo * 100 / TotalEscolasCadastradas) : 0;
}
