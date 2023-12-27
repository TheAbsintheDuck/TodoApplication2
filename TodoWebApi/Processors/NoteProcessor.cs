using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoWebApi.Models;

namespace TodoWebApi.Processors
{
	[ApiController]
	[Route("/notes")]
	public class NoteProcessor
	{
		public static List<Note> notes = new List<Note>();
		public static int counter = 1;

		[HttpGet]
		public List<Note> GetAllNotes()
		{
			return notes;
		}

		[HttpPost]
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
