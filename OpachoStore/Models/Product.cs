using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OpachoStore.Models
{
    public class Product
    {
        public int? ProductID { get; set; }

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Необходимо ввести название товара")]
        public string? Title { get; set; }

        [Required]
        [Range(0.01,999999.99,ErrorMessage = "Необходимо ввести положительную цену(Максимальное число: 999999.99)")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Необходимо задать категорию товара")]
        public string? Category { get; set; }

        public string? Description { get; set; }

        public ICollection<Property> Properties { get; set; } = new List<Property>();

        public string GetStringPrice(bool withoutSign = false)
        {
            string Pr = Price.ToString("0.#############################");

            StringBuilder PriceBuilder = new();
            string PriceResult;
            int j = 0;
            for (int i = Pr.Count() - 1; i >= 0; i--)
            {
                if (j == 3)
                {
                    PriceBuilder.Append(" ");
                    j = 0;
                }
                PriceBuilder.Append(Pr[i]);
                j++;
            }
            PriceResult = new string(PriceBuilder.ToString().Reverse().ToArray());
            if(withoutSign)
            {
                return PriceResult;
            }

            return PriceResult + " ₽";
        }
    }
}
