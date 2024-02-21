using CardGame.BAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CardGame.Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly ICardReader _cardReader;

        public string DisplayString { get; set; }

        [BindProperty]
        public string CardList { get; set; }

        public IndexModel(ILogger<IndexModel> logger,
            ICardReader cardReader)
        {
            _logger = logger;
            _cardReader = cardReader;
            CardList = "2D,3H";
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            try
            {
                DisplayString = _cardReader.ParseCardList(CardList);
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
