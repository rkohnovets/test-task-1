<template>
    <div>
      <h3>Edit Contact</h3>
      <form @submit.prevent="save">
        <label for="name">Name:</label>
        <input type="text" v-model="localContact.fullName" id="name" required />
        
        <label for="email">Email:</label>
        <input type="email" v-model="localContact.email" id="email" required />
        
        <button type="submit">Save</button>
        <button type="button" @click="$emit('cancel')">Cancel</button>
      </form>
    </div>
  </template>
  
  <script>
  export default {
    name: 'EditContact',
    props: {
      contact: Object
    },
    data() {
      return {
        localContact: { ...this.contact } // создаем локальную копию объекта contact
      }
    },
    methods: {
      save() {
        this.$emit('save', this.localContact);
      }
    }
  }
  </script>
  