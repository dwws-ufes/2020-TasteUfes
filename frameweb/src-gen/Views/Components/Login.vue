<template><v-form ref="form">
			<div class="form-group">
				
					
						<v-text-field v-model="email" label="Email" hide-details="auto" class="form-control" required/>
					
				
					
						<v-text-field v-model="password" label="Password" class="form-control" required/>
					
				
					
						<v-btn elevation="2">Login</v-btn>
					
				
			</div>
		</v-form>
	
</template>