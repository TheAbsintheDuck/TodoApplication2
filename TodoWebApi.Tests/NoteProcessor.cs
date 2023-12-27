using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoWebApi.Tests
{
	internal class NoteProcessor
	{
		private List<Note> notes = new List<Note>();
		private int counter = 1;

		public List<Note> GetAllNotes()
		{
			return notes;
		}

		public void Post(Note post)
		{
			if (post == null)
			{
				throw new ArgumentNullException(nameof(post));
			}

			post.Id = counter++;
			notes.Add(post);
		}
	}
}
