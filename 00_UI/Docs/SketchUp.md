
1. StartMenu
	A. Login
		2. Main Menu
			A. CreateAMember
				
				What is your first name?
				What is your last name?
				Password:
				Confirm Password:

				compute FullName

				1. Update Member
				2. Delete Member

			B. Create Book
				Where? - the current member
					if no current member, select one (check if x, if not, select y : METHOD - ForceSelection)
				What?
					Name of Book:
					Initial Balance of Book:
					Note: (optional)

				1. Update Book
				2. Delete Book

			C. Create Bet
				Where? - the current book
					if no current book, select one (METHOD - ForceSelection)
				What? - questions
				How Much? - Amount $
						

				1. Update Bet
				2. Delete Bet

			D. Add Transaction
				Where? - the current book
					if no current book, select one (METHOD - ForceSelection)
				What - Do you want to add funds or withdraw funds?
				How Much? - Amount $

				Update Book - Balance Value (METHOD - Update Book)

				1. Update Transaction
				2. Delete Bet

			E. Add Result
				Where? - the current bet
					if no current bet, select one (METHOD - ForceSelection)
				What - Win/Lose?

				Update Bet - IsResolved (METHOD - Update Bet)
				Update Book - Balance Value (METHOD - Update Book)

				1. Update Result
				2. Delete Result

			F. Reports
				A. See All Items for Member
				B. Analysis Stuff
				C. Lots of Other reports
					1. Financial Statis Report
						A. Overall Balance
						B. Balance Per Book

						Menu - While Loops
							A
							B
							C
							D
							E

							RUN
								LoadSeed
									Login User : Admin
										C# Code Here that logins and get the token

								Login Menu
									1. Login Member
									2. Create New Member
									0. Exit Program

			



			App/Website - Client
				Member Login for Program
			|
			API
			|
			Program - Server
				Admin Login For SQL

				"secrets"
			|
			API
			|
			SQL






