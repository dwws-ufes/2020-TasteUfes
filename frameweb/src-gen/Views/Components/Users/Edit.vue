<template><v-form ref="form">
			<div class="form-group">
				
					
						<v-text-field label="FirstName" hide-details="auto" class="form-control" required/>
					
				
					
						<v-text-field label="LastName" hide-details="auto" class="form-control" required/>
					
				
					
						<v-text-field label="Username" hide-details="auto" class="form-control" required/>
					
				
					
						<v-text-field v-model="email" label="Email" hide-details="auto" class="form-control" required/>
					
				
					
						<v-btn elevation="2">Save</v-btn>
					
				
					
						<v-btn elevation="2">Back</v-btn>
					
				
			</div>
		</v-form>
	
</template>