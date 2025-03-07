<template>
  <div>
    <h2>Добавить задачу</h2>
    <form @submit.prevent="addTask">
      <input v-model="newTask.description" placeholder="Название задачи" required />
      <button type="submit">Добавить</button>
    </form>
  </div>
</template>

<script>
import { defineComponent, ref, inject } from 'vue'

export default defineComponent({
  emits: ['task-added'],
  setup(_, { emit }) {
    const $http = inject('$http')
    const newTask = ref({
      description: '',
      isCompleted: false,
    })

    const addTask = async () => {
      const response = await $http.post('/tasks', newTask.value)
      emit('task-added', response.data)
      newTask.value.description = ''
    }

    return {
      newTask,
      addTask,
    }
  },
})
</script>
