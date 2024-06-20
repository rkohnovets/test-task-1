<template>
    <div>
      <h2>Contractors</h2>
      <ul>
        <li v-for="contractor in contractors" :key="contractor.contractorID" @click="selectContractor(contractor)">
          {{ contractor.contractorName }}
        </li>
      </ul>
    </div>
  </template>
  
  <script>
  export default {
    props: ['contractors'],
    methods: {
      selectContractor(contractor) {
        this.$emit('contractor-selected', contractor);
      }
    }
  }
  </script>
  