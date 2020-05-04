using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Text;
using VideoClub.Infrastructure.Repository.Utils;

namespace VideoClub.Infrastructure.Repository.Extensions
{
    public static class ExceptionExtension
    {
        public static void CustomDescription(this Exception exception)
        {
            switch (exception)
            {
                case DbEntityValidationException entityValidationException:
                {
                    foreach (var eve in entityValidationException.EntityValidationErrors)
                    {
                        Helper.Log.Error(
                            $"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" " +
                            "has the following validation errors:");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Helper.Log.Error($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }

                    break;
                }

                case DbUpdateException updateException:
                {
                    var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");
                    foreach (var result in updateException.Entries)
                    {
                        builder.AppendFormat($"\nType: {result.Entity.GetType().Name} was part of the problem. ");
                        builder.Append($"\nTry {result.State} in database");
                    }

                    builder.Append($"\n{updateException.InnerException?.InnerException?.Message}");
                    Helper.Log.Error(builder.ToString());
                    break;
                }

                default:
                    Helper.Log.Error(exception.Message);
                    break;
            }
        }
    }
}
