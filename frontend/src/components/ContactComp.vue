<template>
    <div v-if="contact">
        {{ contact.fullName }} - {{ contact.email }}
        <button @click="editContact(contact)">Edit</button>
        <button @click="deleteContact(contact.contactID)">Delete</button>
    </div>
    <LoaderComp v-else/>

    <EditContact 
      v-if="showEditForm"
      :contactId="props.contactId"
      :contractorId="props.contractorId"
      @contactUpdated="emit('contactUpdated')" 
      @cancel="cancelEdit" 
    />
</template>

<script setup>
import { ref } from 'vue';
import { defineProps, defineEmits, onMounted } from 'vue';
import EditContact from './EditContact.vue';
import { API_BASE_URL } from '../apiConfig';
import LoaderComp from './LoaderComp.vue';

const props = defineProps({
    contactId: Number,
    contractorId: Number
})

const emit = defineEmits(['contactDeleted', 'contactUpdated'])

const contact = ref(null)
const showEditForm = ref(false)

const fetchContact = async () => {
    const response = await fetch(`${API_BASE_URL}/api/contacts/${props.contactId}`);
    contact.value = await response.json();
}

const deleteContact = async () => {
  try {
    await fetch(`${API_BASE_URL}/api/contacts/${props.contactId}`, {
      method: 'DELETE'
    });
    emit('contactDeleted');
  } catch (error) {
    console.error('Error deleting contact:', error);
  }
};

const editContact = () => {
  showEditForm.value = true;
};
const cancelEdit = () => {
  showEditForm.value = false;
};

onMounted(fetchContact);

</script>

<style>

</style>