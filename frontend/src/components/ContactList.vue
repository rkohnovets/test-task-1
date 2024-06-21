<template>
  <div>
    <h2>Contacts</h2>
    <ul>
      <li v-for="contact in contacts" :key="contact.contactID">
        {{ contact.name }} - {{ contact.email }}
        <button @click="deleteContact(contact.contactID)">Delete</button>
        <button @click="editContact(contact)">Edit</button>
      </li>
    </ul>
    <add-contact :contractors="contractors" @contact-added="fetchContacts"></add-contact>
  </div>
</template>

<script>
import AddContact from './AddContact.vue';

export default {
  props: {
    contacts: Array,
    contractors: Array
  },
  components: {
    AddContact
  },
  methods: {
    async deleteContact(contactID) {
      try {
        await fetch(`http://localhost:5000/api/contacts/${contactID}`, {
          method: 'DELETE'
        });
        this.$emit('contact-deleted');
      } catch (error) {
        console.error('Error deleting contact:', error);
      }
    },
    editContact(contact) {
      this.$emit('edit-contact', contact);
    }
  }
};
</script>
