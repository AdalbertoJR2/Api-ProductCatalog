using Flunt.Notifications;
using Flunt.Validations;

namespace ProductCatalog.ViewModels.ProdutoViewModels
{
    public class EditorProdutoViewModel : Notifiable, IValidatable
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public string Imagem { get; set; }
        public int CategoriaId { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .HasMaxLen(Titulo, 120, "Titulo", "O título deve conter até 120 caracteres")
                    .HasMinLen(Titulo, 3, "Titulo", "O título deve conter pelo menos 3 caracteres")
                    .IsGreaterThan(Preco, 0, "Preço", "O preço deve ser maior que zero")
            );
        }
    }
}