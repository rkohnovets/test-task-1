<template>
  <div>
    <h2>Add Contact</h2>
    <form @submit.prevent="addContact">
      <div>
        <label for="name">Name:</label>
        <input type="text" v-model="newContact.name" required />
      </div>
      <div>
        <label for="email">Email:</label>
        <input type="email" v-model="newContact.email" required />
      </div>
      <div>
        <label for="contractor">Contractor:</label>
        <select v-model="newContact.contractorID" required>
          <option v-for="contractor in contractors" :key="contractor.contractorID" :value="contractor.contractorID">
            {{ contractor.name }}
          </option>
        </select>
      </div>
      <button type="submit">Add Contact</button>
    </form>
  </div>
</template>

<script>
export default {
  props: {
    contractors: Array
  },
  data() {
    return {
      newContact: {
        name: '',
        email: '',
        contractorID: null
      }
    };
  },
  methods: {
    async addContact() {
      try {
        await fetch('http://localhost:5000/api/contacts', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(this.newContact)
        });
        this.$emit('contact-added');
      } catch (error) {
        console.error('Error adding contact:', error);
      }
    }
  }
};
</script>
