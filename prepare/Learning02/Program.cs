using System;

class Program
{
  static void Main(string[] args)
  {
    Job job1 = new Job();
    job1._jobTitle = "Software Engineer";
    job1._company = "Tesla";
    job1._startYear = 3000;
    job1._endYear = 3003;

    Job job2 = new Job();
    job2._jobTitle = "CEO";
    job2._company = "SpaceX";
    job2._startYear = 3003;
    job2._endYear = 3010;

    Resume resume = new Resume();
    resume._name = "Egor Sotnikov";
    resume._jobs.Add(job1);
    resume._jobs.Add(job2);

    resume.Display();
  }
}