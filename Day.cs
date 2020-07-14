using System;
using System.Collections.Generic;
using System.Text;

namespace EmoDia
{
    public class EmotionalNote
    {
        public string time;
        public string situation;
        public string reaction;
        public string emotion;
        public string trigger;
        public string feelingsAtTheMoment;
        public string rethinking;
        public string conclusion;

        public EmotionalNote (string time, string situation, string reaction, string emotion, string trigger, string feelingsAtTheMoment,
            string rethinking, string conclusion)
        {
            this.time = time;
            this.situation = situation;
            this.reaction = reaction;
            this.emotion = emotion;
            this.trigger = trigger;
            this.feelingsAtTheMoment = feelingsAtTheMoment;
            this.rethinking = rethinking;
            this.conclusion = conclusion;
        }
    }

    public class ThankingNote
    {
        public string gratitude;

        public ThankingNote (string gratitude)
        {
            this.gratitude = gratitude;
        }
    }

    public class PersonalNote
    {
        public string note;
        List<string> documents = new List<string>();    //??????????????/

        public PersonalNote(string note, List<string> documents)
        {
            this.note = note;
            this.documents = documents;

        }
    }

    public class Day
    {
        public DateTime date;
        public List<EmotionalNote> emotionalNotes;
        public List<ThankingNote> thankingNotes;
        public List<PersonalNote> personalNotes;

        public Day(DateTime date, List<EmotionalNote> emotionalNotes, List<ThankingNote> thankingNotes, List<PersonalNote> personalNotes)
        {
            this.date = date;
            this.emotionalNotes = emotionalNotes;
            this.thankingNotes = thankingNotes;
            this.personalNotes = personalNotes;
        }
    } 
}
