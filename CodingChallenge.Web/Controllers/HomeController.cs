using System;
using System.Linq;
using System.Web.Mvc;
using CodingChallenge.Business;

namespace CodingChallenge.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Reverse word order: e.g. ‘the brown fox’ becomes ‘fox brown the’
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns>reverse word order</returns>
        public JsonResult ReverseWordOrder(string paragraph)
        {
            if (!String.IsNullOrEmpty(paragraph))
            {
                var reverse = String.Join(" ", paragraph.Split(' ').Reverse());
                return Json(reverse, JsonRequestBehavior.AllowGet);
            }
            return Json("paragraph could not be null or empty, try again", JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Reverse characters while maintaining word order: e.g. ‘the brown fox’ becomes ‘eht nworb xof’
        /// </summary>
        public JsonResult ReverseCharactersWithSameWordOrder(string paragraph)
        {
            if (!String.IsNullOrEmpty(paragraph))
            {
                string[] words = paragraph.Split(' ');
                string result = "";
                foreach (var word in words)
                {
                    result += String.Join("", word.Reverse()) + " ";
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json("Paragraph could not be null or empty, try again", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SortingAlphabetically(string paragraph)
        {
            if (!String.IsNullOrEmpty(paragraph))
            {
                string[] sortingAlphabetically = paragraph.Split(' ');
                var asc = from s in sortingAlphabetically
                          orderby s ascending
                          select s;
                var result = String.Join(" ", asc);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json("Paragraph could not be null or empty, try again", JsonRequestBehavior.AllowGet);
        }

        public JsonResult Encrypt(string paragraph)
        {
            if (!String.IsNullOrEmpty(paragraph))
            {
                Utils utilHash = new Utils();
                var hashValue = utilHash.GenerateHash(paragraph);
                return Json(hashValue, JsonRequestBehavior.AllowGet);
            }
            return Json("Paragraph could not be null or empty, try again", JsonRequestBehavior.AllowGet);
        }

        //Create an amortization table based on inputs for:
        //original loan amount, loan term(in months) and interest rate 
        //and return a fully amortized payment schedule showing:
        //payment amount, principal paid and balance remaining on a monthly basis.
        public JsonResult Calculator(string originalLoanAmount, string loanTerm, string interestRate)
        {
            if (!String.IsNullOrEmpty(originalLoanAmount)
                && !String.IsNullOrEmpty(loanTerm)
                && !String.IsNullOrEmpty(interestRate))
            {
                // (Loan Value) * (1 + r/12) ^ p = (12x / r) * ((1 + r/12)^p - 1)
                // payment = (((Loan Value) * (1 + r/12) ^ p) * r)/ (12 * ((1 + r/12)^p - 1)));
                // price of total mortgage before down payment
                double loanAmount = Convert.ToDouble(originalLoanAmount);
                // down payment will be subtracted from the loan: according to the question, there is no downPayment
                double downPayment = 0;
                // calculate interest from 100%
                double dInterestRate = Convert.ToDouble(interestRate) / 100;
                // monthly term
                double termOfLoan = Convert.ToDouble(loanTerm);
                // plug the values from the input into the mortgage formula
                double payment = (loanAmount - downPayment) * (Math.Pow((1 + dInterestRate / 12), termOfLoan) * dInterestRate) / (12 * (Math.Pow((1 + dInterestRate / 12), termOfLoan) - 1));
                // add on a monthly 
                //payment = payment / 12;
                string sPayment = String.Format("{0:0.##}", payment);                
                var result = new { MonthlyPayment = sPayment };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json("Error, please try again with all the 3 values", JsonRequestBehavior.AllowGet);
        }
    }
}