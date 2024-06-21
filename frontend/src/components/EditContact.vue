<template>
  <div>
    <h2>Edit Contact</h2>
    <form @submit.prevent="saveContact">
      <div>
        <label for="name">Name:</label>
        <input type="text" v-model="contact.fullName" required />
      </div>
      <div>
        <label for="email">Email:</label>
        <input type="email" v-model="contact.email" required />
      </div>
      <button type="submit">Save</button>
      <button type="button" @click="cancel">Cancel</button>
    </form>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { defineProps, defineEmits } from 'vue';
import { API_BASE_URL } from '../apiConfig';

const props = defineProps({
  contactId: Number,
  contractorId: Number
});

const emit = defineEmits(['contactUpdated', 'cancel']);

const contact = ref({
  fullName: '',
  email: '',
  contactID: props.contactId,
  contractorId: props.contractorId
});

const saveContact = async () => {
  try {
    await fetch(`${API_BASE_URL}/api/contacts/${props.contactId}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(contact.value)
    });
    emit('contactUpdated');
  } catch (error) {
    console.error('Error saving contact:', error);
  }
};

const cancel = () => {
  emit('cancel');
};
</script>
