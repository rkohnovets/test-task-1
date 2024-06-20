<template>
  <div>
    <h2>Contacts</h2>
    <ul>
      <li v-for="contact in contacts" :key="contact.contactID">
        {{ contact.fullName }} ({{ contact.email }})
        <button @click="editContact(contact)">Edit</button>
      </li>
    </ul>
    <edit-contact v-if="editingContact" :contact="editingContact" @save="saveContact" @cancel="cancelEdit"/>
  </div>
</template>

<script>
import EditContact from './EditContact.vue';

export default {
  name: 'ContactList',
  components: {
    EditContact
  },
  props: {
    contacts: Array
  },
  data() {
    return {
      editingContact: null
    }
  },
  methods: {
    editContact(contact) {
      this.editingContact = { ...contact };
    },
    saveContact(contact) {
      this.$emit('save-contact', contact);
      this.editingContact = null;
    },
    cancelEdit() {
      this.editingContact = null;
    }
  }
}
</script>
