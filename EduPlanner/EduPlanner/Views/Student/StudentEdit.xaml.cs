﻿using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EduPlanner.Views.Student
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentEdit : ContentPage
    {
        MediaFile file;
        StudentRepository studentRepository = new StudentRepository();
        public StudentEdit(StudentModel student)
        {
            InitializeComponent();
            TxtEmail.Text = student.Email;
            TxtName.Text = student.Name;
            TxtId.Text = student.Id;
            //Esto es mio 
        }

        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            string name = TxtName.Text;
            string email = TxtEmail.Text;
            if (string.IsNullOrEmpty(name))
            {
                await DisplayAlert("Advertencia", "Por favor, ingresa tu nombre.", "Ok");
            }
            if (string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Advertencia", "Por favor, ingresa tu correo.", "Ok");
            }

            StudentModel student = new StudentModel();
            student.Id = TxtId.Text;
            student.Name = name;
            student.Email = email;
            if (file != null)
            {
                string image = await studentRepository.Upload(file.GetStream(), Path.GetFileName(file.Path));
                student.Image = image;
            }
            if (file != null)
            {
                string image = await studentRepository.Upload(file.GetStream(), Path.GetFileName(file.Path));
                student.Image = image;
            }
            bool isUpdated = await studentRepository.Update(student);
            if (isUpdated)
            {
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo actualizar la información", "Ok");
            }
        }

        //private async void ImageTap_Tapped(object sender, EventArgs e)
        //{
        //    await CrossMedia.Current.Initialize();
        //    try
        //    {
        //        file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
        //        {
        //            PhotoSize = PhotoSize.Medium
        //        });
        //        if (file == null)
        //        {
        //            return;
        //        }
        //        StudentImage.Source = ImageSource.FromStream(() =>
        //        {
        //            return file.GetStream();
        //        });
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

    }
}