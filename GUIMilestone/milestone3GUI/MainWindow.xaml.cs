using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;

namespace milestone3GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User newUser;
        Business newBusiness;
        List<String> categoryList;

        public MainWindow()
        {
            //make window resizable false 
            this.ResizeMode = 0;
            InitializeComponent();
            AddColumnsToGrid();
            AddColumnsToGridSearch();
            AddStates();
            AddSort();
            GetCategories();
            AddRating();
        }

        /**
         * Description: Gets the list of categories that exists in the whole database.
         * Return: Returns the list of categories. 
         */ 
        public void GetCategories()
        {
            Categories newCategory = new Categories();
            List<String> categoriesList = newCategory.GetCategories("");
            foreach(String item in categoriesList)
            {
                categoryListBox.Items.Add(item);
            }
        }

        public void AddRating()
        {
            ratingComboBox.Items.Add("1");
            ratingComboBox.Items.Add("2");
            ratingComboBox.Items.Add("3");
            ratingComboBox.Items.Add("4");
            ratingComboBox.Items.Add("5");
            ratingComboBox.SelectedIndex = 4;
        }

        /**
         * Description: Adds items in the sortComboBox 
         */
        public void AddSort()
        {
            sortByComboBox.Items.Add("Name (Default)");
            sortByComboBox.Items.Add("Distance");
            sortByComboBox.Items.Add("Stars");
            sortByComboBox.Items.Add("# of Reviews");
            sortByComboBox.Items.Add("Avg Review Rating");
            sortByComboBox.Items.Add("Total Checkins");
            sortByComboBox.SelectedItem = "Name (Default)";
        }

        /**
         * Description: Adds the all the states from the database when the program loads. 
         */
        public void AddStates()
        {
            newBusiness = new Business();
            List<String> states = newBusiness.GetLocation("state", " ", "state");
            foreach(String item in states)
            {
                stateList.Items.Add(item);
            }
        }

        /**
         * Description: Adds all the zipcodes from a specific chosen city. 
         */
        public void AddZipcodes()
        {
            if (cityListBox.SelectedIndex > -1)
            {
                zipcodeListBox.Items.Clear();
                List<String> zipCodes = newBusiness.GetLocation("zipcode", " WHERE city = '" + cityListBox.SelectedItem.ToString() + "' ", "zipcode");
                foreach (String item in zipCodes)
                {
                    zipcodeListBox.Items.Add(item);
                }
            }
            
        }

        /**
         * Description: Adds all the cities based on the selected state
         */
        public void AddCities()
        {
            if (stateList.SelectedIndex > -1)
            {
                cityListBox.Items.Clear();
                List<String> cities = newBusiness.GetLocation("city", " WHERE state = '" + stateList.SelectedItem.ToString() + "' ", "city");
                foreach (String item in cities)
                {
                    cityListBox.Items.Add(item);
                }
            }
        }

        /**
         * Description: Adds all the columns that is needed for the reviewDataGrid 
         */
        public void AddReviewColumns(DataGrid dg)
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Date";
            col1.Binding = new Binding("date");
            col1.Width = 100;
            dg.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = "User Name";
            col2.Binding = new Binding("user_id");
            col2.Width = 75;
            dg.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Header = "Stars";
            col3.Binding = new Binding("stars");
            col3.Width = 100;
            dg.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Header = "Text";
            col4.Binding = new Binding("text");
            col4.Width = 250;
            dg.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Header = "Funny";
            col5.Binding = new Binding("funny_vote");
            col5.Width = 100;
            dg.Columns.Add(col5);

            DataGridTextColumn col6 = new DataGridTextColumn();
            col6.Header = "Useful";
            col6.Binding = new Binding("useful_vote");
            col6.Width = 100;
            dg.Columns.Add(col6);

            DataGridTextColumn col7 = new DataGridTextColumn();
            col7.Header = "Cool";
            col7.Binding = new Binding("cool_vote");
            col7.Width = 100;
            dg.Columns.Add(col7);
        }

        /**
         * Description: Adds all the columns that are needed for the business data grid 
         */
        public void AddColumnsToGridSearch()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "BusinessName";
            col1.Binding = new Binding("name");
            col1.Width = 100;
            searchDataGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = "Address";
            col2.Binding = new Binding("address");
            col2.Width = 150;
            searchDataGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Header = "City";
            col3.Binding = new Binding("city");
            col3.Width = 50;
            searchDataGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Header = "State";
            col4.Binding = new Binding("state");
            col4.Width = 50;
            searchDataGrid.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Header = "Distance";
            col5.Binding = new Binding("is_open");
            col5.Width = 50;
            searchDataGrid.Columns.Add(col5);

            DataGridTextColumn col6 = new DataGridTextColumn();
            col6.Header = "Stars";
            col6.Binding = new Binding("stars");
            col6.Width = 50;
            searchDataGrid.Columns.Add(col6);

            DataGridTextColumn col7 = new DataGridTextColumn();
            col7.Header = "# of Reviews";
            col7.Binding = new Binding("review_count");
            col7.Width = 50;
            searchDataGrid.Columns.Add(col7);

            DataGridTextColumn col8 = new DataGridTextColumn();
            col8.Header = "Avg Review Rating";
            col8.Binding = new Binding("reviewRating");
            col8.Width = 50;
            searchDataGrid.Columns.Add(col8);

            DataGridTextColumn col9 = new DataGridTextColumn();
            col9.Header = "Total Checkin";
            col9.Binding = new Binding("num_checkins");
            col9.Width = 50;
            searchDataGrid.Columns.Add(col9);
        }

        /**
         * Description: Enables the textboxes for the user latitude and longitude for editing 
         */
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            latTextBox.IsEnabled = true;
            longTextBox.IsEnabled = true;
            latTextBox.Focus();
            latTextBox.SelectionStart = 0;
            latTextBox.SelectionLength = latTextBox.Text.Length;
        }

        /**
         * Description: Updates the user latitude and longitude and disables the textboxes 
         */
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            latTextBox.IsEnabled = false;
            longTextBox.IsEnabled = false;
            newUser.user_latitude = Double.Parse(latTextBox.Text);
            newUser.user_longitude = Double.Parse(longTextBox.Text);
            newUser.UpdateUserLocation(newUser);
        }

        /**
         * Description: Adds the columns for the friend data grid 
         */
        public void AddColumnsToGrid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Header = "Name";
            col1.Binding = new Binding("name");
            col1.Width = 100;
            friendGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Header = "Avg Stars";
            col2.Binding = new Binding("average_stars");
            col2.Width = 75;
            friendGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Header = "Yelping Since";
            col3.Binding = new Binding("yelping_since");
            col3.Width = 100;
            friendGrid.Columns.Add(col3);
        }
        

        public void GetReviews() { }

        public void GetRatings() { }


        /**
         * Description: Gets all the users that match the name in the userTextBox 
         */
        public void GetAllUsers()
        {
            userTextField.Items.Clear();
            String currentUser = userTextBox.Text;
            newUser = new User();
            List<String> users = newUser.GetUsers(currentUser);
            foreach (String item in users)
            {
                userTextField.Items.Add(item);
            }
        }

        /**
         * Description: Gets users that matches name everytime a character is typed in 
         */
        private void UserTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetAllUsers();
        }

        /**
         * Description: Gets the list of friends of a current user 
         */
        public void GetUsersFriends()
        {
            friendGrid.Items.Clear();
            List<String> userFriendsList = new List<String>();
            Friends userFriends = new Friends();
            userFriendsList = userFriends.GetListOfFriends(newUser.user_id);
            User friend;
            foreach (String item in userFriendsList)
            {
                friend = userFriends.GetFriend(item);
                friendGrid.Items.Add(new User { name = friend.name, average_stars = friend.average_stars, yelping_since = friend.yelping_since}); 
            } 
        }

        public void PopulateUserTextField()
        {
            newUser.user_id = userTextField.SelectedItem.ToString();
            newUser.SetUserInformation();
            nameTextBox.Text = newUser.name;
            starsTextBox.Text = newUser.average_stars.ToString("0.00");
            fansTextBox.Text = newUser.fans.ToString();
            yelpingTextBox.Text = newUser.yelping_since;
            coolTextBox.Text = newUser.cool.ToString();
            funnyTextBox.Text = newUser.funny.ToString();
            usefulTextBox.Text = newUser.useful.ToString();
            latTextBox.Text = newUser.user_latitude.ToString("0.00000");
            longTextBox.Text = newUser.user_longitude.ToString("0.00000");
            GetUsersFriends();
        }

        /**
         * Description: When a user_id is selected, this function will query and that user_id and assign all of its information.
         *              This function assignes those user information to the textboxes in the GUI 
         */
        private void UserTextField_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(userTextField.SelectedIndex > -1)
            {
                PopulateUserTextField();
            }
            
        }

       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(newUser != null)
            {
                Review newReview = new Review();
                SimpleHasher hash = new SimpleHasher();
                DateTime thisDay = DateTime.Today;

                newReview.text = reviewTextBox.Text;
                newReview.review_Id = hash.GetHash();
                newReview.user_id = newUser.user_id;
                newReview.stars = ratingComboBox.SelectedIndex + 1;
                newReview.business_id = newBusiness.business_Id;
                newReview.date = thisDay;
                newReview.funny_vote = 0;
                newReview.cool_vote = 0;
                newReview.useful_vote = 0;
                newReview.InsertReview(newReview);
                reviewTextBox.Text = "";
            }
            else
            {
                reviewTextBox.Text = "Log in to add review";
            }
        }

        /**
        * Description: When the state selection changes the cities are added and the zipcode list 
        *              box is cleared. 
        */
        private void StateList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddCities();
            zipcodeListBox.Items.Clear();
        }

        /**
        * Description: 
        */
        public void GetBusiness()
        {
            categoryList = new List<String>();
            Checkins newCheckin = new Checkins();
            if (searchListBox.Items.Count > 0)
            {
                foreach(String item in searchListBox.Items)
                {
                    categoryList.Add(item);
                }
            }
            newBusiness = new Business();
            String stateItem = stateList.SelectedItem.ToString();
            String cityItem = cityListBox.SelectedItem.ToString();
            String zipcodeItem = zipcodeListBox.SelectedItem.ToString();
            List<Business> businessList = newBusiness.GetBusinesses(stateItem, cityItem, zipcodeItem, "name", categoryList, categoryList);
            foreach(Business item in businessList)
            {
                //newCheckin.SetTotalCheckins(item);
                searchDataGrid.Items.Add(new Business()
                {
                    name = item.name,
                    address = item.address,
                    city = item.city,
                    state = item.state,
                    stars = item.stars,
                    review_count = item.review_count,
                    review_rating = item.review_rating,
                    num_checkins = item.num_checkins,
                    business_Id = item.business_Id
                });
            }
            


            //businessCategoryLabel.Text = newUser.name;
        }

        /**
        * Description: When the search button is pressed this function checks if the state, city and
        *              zipcode are filled up. If they are, a business query will be made with respect
        *              to the selected location details. This will also clear the business grid no 
        *              matter what. 
        */
        private void Button_Click_1(object sender, RoutedEventArgs e)
        { 
            searchDataGrid.Items.Clear();
            if(cityListBox.SelectedIndex > -1 && stateList.SelectedIndex > -1 && zipcodeListBox.SelectedIndex > -1)
            {
                GetBusiness();     
            }
            
        }

        public void SetTotalCheckins()
        {
            Checkins newCheckin = new Checkins();
            newCheckin.SetTotalCheckins(newBusiness); 
        }

        /**
        * Description: This function will call the AddZipcodes that will populate the zipcode
        *              list box when a specific state and city is selected. 
        */
        private void CityListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddZipcodes();
        }

        /**
        * Description: Adds the selected category from the category list box to the search
        *              list box. Does not allow duplicate categories. 
        */
        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if(categoryListBox.SelectedIndex > -1 && !searchListBox.Items.Contains(categoryListBox.SelectedItem))
            {           
                    String item = categoryListBox.SelectedItem.ToString();
                    searchListBox.Items.Add(item);       
            } 
        }

        /**
        * Description: Removes a category from the search list box. 
        */
        private void RemoveCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            searchListBox.Items.Remove(searchListBox.SelectedItem);
        }

        /**
        * Description: If there are items on the business grid, this function will
        *              get the current business selected and display all of its information.
        *              
        */
        private void SearchDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(searchDataGrid.SelectedIndex > -1)
            {
                Business test = (Business)searchDataGrid.SelectedItem;
                businessNameLabel.Text = test.name;
                businessAddressLabel.Text = test.address;
                //businessCategoryLabel.Text = newUser.name;
                Categories businessCategory = new Categories();
                categoryList = businessCategory.GetCategories(test.business_Id);
                categoryLabel.Text = "";
                foreach (String item in categoryList)
                    categoryLabel.Text += item + "\n";
                newBusiness = new Business();
                newBusiness.business_Id = test.business_Id;
                Hours newHours = new Hours();
                DateTime thisDay = DateTime.Today; //make this a uninstantiated class variable.
                newHours = newHours.GetBusinessHours(newBusiness.business_Id, DateTime.Today);
                hoursLabel.Text = newHours.day.ToString("dddd") +'\n' + "Open: " + newHours.open.ToString("hh:mm") + '\n' + "Close: " + newHours.closed.ToString("hh:mm");
                reviewTextBox.Text = "";
            }
            
        }

        /**
        * Description: When clicked it will make a query on all of the reviews that 
        *              match the business id and will display all the details of 
        *              the reviews in a separate window. 
        */
        private void ShowReviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (searchDataGrid.SelectedIndex > -1)
            {
                Window reviewWindow = new Window();
                reviewWindow.Title = "Review by users";
                reviewWindow.ResizeMode = 0;
                DataGrid reviewDataGrid = new DataGrid();
                StackPanel panel = new StackPanel();
                panel.Children.Add(reviewDataGrid);
                Review newReview = new Review();
                List<Review> reviewList = newReview.GetReviews(newBusiness.business_Id);
                reviewWindow.SizeToContent = SizeToContent.WidthAndHeight;

                foreach (Review item in reviewList)
                {
                    reviewDataGrid.Items.Add(new Review {
                        date = item.date,
                        user_id = item.user_id,
                        stars = item.stars,
                        text = item.text,
                        funny_vote = item.funny_vote,
                        useful_vote = item.useful_vote,
                        cool_vote = item.cool_vote
                    });
                }

            AddReviewColumns(reviewDataGrid);
            reviewWindow.Content = panel;
            reviewWindow.Show();
            }
        }

        private void CityListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddZipcodes();
        }

        private void CheckinButton_Click(object sender, RoutedEventArgs e)
        {
            Checkins newCheckins = new Checkins();
            DateTime thisDay = DateTime.Today;
            newCheckins.business_id = newBusiness.business_Id;
            newCheckins.count++;
            newCheckins.day = DateTime.Now;
            newCheckins.time = DateTime.Now;
            //hoursLabel.Text = newCheckins.time + '\n' + newBusiness.business_Id + '\n' + newCheckins.day;
            newCheckins.UpdateCheckins(newCheckins);
        }

        private void UserTextField_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userTextField.SelectedIndex > -1)
            {
                PopulateUserTextField();
            }
        }

        private void SearchDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //do nothing 
        }
    }
}
