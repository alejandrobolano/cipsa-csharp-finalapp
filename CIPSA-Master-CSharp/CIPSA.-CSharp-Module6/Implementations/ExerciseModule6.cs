using CIPSA._CSharp_Module6.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPSA._CSharp_Module6.Implementations
{
    public class ExerciseModule6
    {
        private IExercisesModule6 _exerciseModule6;
        public ExerciseModule6(IExercisesModule6 exercisesModule6)
        {
            _exerciseModule6 = exercisesModule6;
        }

        public void ExecuteExercise()
        {
            _exerciseModule6.ExecuteExercise();
        }
    }
}
