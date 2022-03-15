using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBFitness.BL.Model;

namespace CBFitness.BL.Controller
{
    public class ExerciseController : BaseController
    {
        private readonly User user;
        public List<Exercise> ExercisesLst { get; }
        private const string EXERCISES_FILE_NAME = "exercises.dat";
        private const string ACTIVITY_FILE_NAME = "activity.dat";
        public List<Activity> ActivityLst { get; }
        public ExerciseController(User user)
        {
            this.user = user ?? throw new ArgumentNullException(nameof(user),"Incorrcet user, cant be null") ;
            ExercisesLst = GetAllExercises();
            ActivityLst = GetAllActivity();
        }

        private List<Activity> GetAllActivity()
        {
            return Load<List<Activity>>(ACTIVITY_FILE_NAME) ?? new List<Activity>();
        }

        public void Add(Activity activity, DateTime begin, DateTime finish)
        {
            var act = ActivityLst.SingleOrDefault(a => a.Name == activity.Name);
            if (act == null)
            {
                ActivityLst.Add(activity);
                var exercise = new Exercise(begin,finish,activity,user);
                ExercisesLst.Add(exercise);
            }
            else
            {
                var exercise = new Exercise(begin, finish, act, user);
                ExercisesLst.Add(exercise);
            }
            Save();
        }
        private List<Exercise> GetAllExercises()
        {
            return Load<List<Exercise>>(EXERCISES_FILE_NAME) ?? new List<Exercise>();
        }

        private void Save()
        {
            Save(EXERCISES_FILE_NAME, ExercisesLst);
            Save(ACTIVITY_FILE_NAME, ActivityLst);
        }
    }
}
