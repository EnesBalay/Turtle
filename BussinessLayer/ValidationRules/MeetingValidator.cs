using EntityLayer.Concrete;
using FluentValidation;

namespace BussinessLayer.ValidationRules
{
    public class MeetingValidator : AbstractValidator<Meeting>
    {
        public MeetingValidator()
        {
            RuleFor(x => x.MeetingName).MinimumLength(3).WithMessage("Toplantı başlığı en az 3 karakter olmalıdır.");
            //RuleFor(x => x.PlanningDate).NotEmpty().WithMessage("Planlanan tarih boş geçilemez.");
            RuleFor(x => x.MeetingName).MinimumLength(3).WithMessage("Toplantı başlığı en az 3 karakter olmalıdır.");
            //RuleFor(x => x.PlanningDate).GreaterThan(DateTime.Now).WithMessage("Toplantı geçmiş bir zamanda olamaz.");
            //RuleFor(x => x.PlanningDate2).GreaterThan(DateTime.Now).WithMessage("Toplantı geçmiş bir zamanda olamaz.");
            //RuleFor(x => x.PlanningDate3).GreaterThan(DateTime.Now).WithMessage("Toplantı geçmiş bir zamanda olamaz.");
        }
    }
}
