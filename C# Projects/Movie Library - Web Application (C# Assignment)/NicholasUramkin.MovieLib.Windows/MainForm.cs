﻿/* Nicholas Uramkin
 * Lab 4
 * ITSE 1430
 * 4/16/2018
 * MainForm.cs
 * */

using System;
using System.Windows.Forms;
using NicholasUramkin.MovieLib.Data.Memory;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NicholasUramkin.MovieLib.Data;
using NicholasUramkin.MovieLib.Data.Sql;

namespace NicholasUramkin.MovieLib.Windows
{
    /// <summary>Class for MainForm</summary>
    public partial class MainForm : Form
    {
        /// <summary>MainForm default constructor</summary>
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            var connString = ConfigurationManager.ConnectionStrings["MovieDatabase"];
            _database = new SqlMovieDatabase(connString.ConnectionString);

            RefreshUI();
        }

        //exit application
        private void OnFileExit( object sender, EventArgs e )
        {
            Close();
        }

        private void OnMovieAdd( object sender, EventArgs e )
        {
 
           //instatiate MovieDetailForm with Add Movie in titlebar
           var form = new MovieDetailForm("Add Movie");

           //show MovieDetailForm
           var result = form.ShowDialog(this);
           //return if OK(Save) not selected
           if (result != DialogResult.OK)
                    return;
            
            //temporarily store form data incase save fails
            var temp = form.Movie;

            //basically do while no exceptions 
            //(break will only execute if there is no exception thrown)
            do
            {
                //add to database
                try
                {
                    _database.Add(form.Movie);
                    break;
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    form = new MovieDetailForm(temp, "Add Movie");
                    result = form.ShowDialog(this);
                    if (result != DialogResult.OK)
                        return;
                }
            } while (true);

            RefreshUI();
        }

        private void OnMovieEdit( object sender, EventArgs e )
        {
            //get selected movie
            var movie = GetSelectedMovie();
            if (movie == null)
            {
                MessageBox.Show(this, "No movie selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            EditMovie(movie);
        }

        private void OnMovieRemove( object sender, EventArgs e )
        {
            var movie = GetSelectedMovie();
            if (movie == null)
            {
                MessageBox.Show(this, "No movie selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DeleteMovie(movie);
        }

        //show about window
        private void OnHelpAbout( object sender, EventArgs e )
        {
            var form = new AboutBox();

            form.ShowDialog();
        }

        private void EditMovie(Movie movie)
        {
                var form = new MovieDetailForm(movie);
                var result = form.ShowDialog(this);
                if (result != DialogResult.OK)
                    return;
            do
            { 
                //update movie
                form.Movie.Id = movie.Id;

                var temp = form.Movie;

                try
                {
                    _database.Update(form.Movie);
                    break;
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    form = new MovieDetailForm(temp);
                    result = form.ShowDialog(this);
                    if (result != DialogResult.OK)
                        return;
                }
            } while (true);

            RefreshUI();
        }

        private void DeleteMovie(Movie movie)
        {
            if (!ShowConfirmation("Are you sure?", "Remove Movie"))
                return;

            //remove movie
            try
            {
                _database.Remove(movie.Id);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            RefreshUI();
        }

        //show confirmation window
        private bool ShowConfirmation( string message, string title )
        {
            //return true if Yes
            return MessageBox.Show(this, message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private void RefreshUI()
        {
            ////get movies
            //var movies = _database.GetAll();

            ////bind to grid
            //movieBindingSource.DataSource = movies.ToList();

            //get movies
            IEnumerable<Movie> movies = null;
            try
            {
                movies = _database.GetAll();
            } catch (Exception)
            {
                MessageBox.Show("Error loading movies");
            }

            movieBindingSource.DataSource = movies?.ToList();
        }

        private Movie GetSelectedMovie()
        {
            //get the first selected row in grid, if there is any
            //if (dataGridView1.SelectedRows.Count > 0)
            //    return dataGridView1.SelectedRows[0].DataBoundItem as Movie;

            //return null;

            var items = (from r in dataGridView1.SelectedRows.OfType<DataGridViewRow>()
                         select new {
                             Index = r.Index,
                             Movie = r.DataBoundItem as Movie
                         }).FirstOrDefault();

            return items.Movie;
        }

        private void OnCellDoubleClick( object sender, DataGridViewCellEventArgs e )
        {
            var movie = GetSelectedMovie();
            if (movie == null)
                return;

            EditMovie(movie);
        }

        private void OnCellKeyDown( object sender, KeyEventArgs e )
        {
            var movie = GetSelectedMovie();
            if (movie == null)
                return;

            if(e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                DeleteMovie(movie);
            }
            else if(e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                EditMovie(movie);
            }
        }

        private IMovieDatabase _database;// = new MemoryMovieDatabase();
    }
}
