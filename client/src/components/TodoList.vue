<template>
  <div>
    <h2>Список задач</h2>
    <ul>
      <li v-for="task in tasks" :key="task.id">
        <input type="checkbox" :checked="task.isCompleted" @change="toggleCompletion(task)" />
        <span :class="{ completed: task.isCompleted }">{{ task.description }}</span>
        <button @click="deleteTask(task.id)">Удалить</button>
      </li>
    </ul>
  </div>
</template>

<script>
import { defineComponent, inject } from 'vue'

export default defineComponent({
  props: {
    tasks: {
      type: Array,
      required: false,
      default: () => [],
    },
  },
  emits: ['task-deleted'],
  setup(props, { emit }) {
    const $http = inject('$http')
    const toggleCompletion = async (task) => {
      task.isCompleted = !task.isCompleted
      await $http.put(`/tasks/complete/${task.id}`, task)
    }

    const deleteTask = async (id) => {
      await $http.delete(`/tasks/${id}`)
      emit('task-deleted', id)
    }

    return {
      toggleCompletion,
      deleteTask,
    }
  },
})
</script>

<style>
.completed {
  text-decoration: line-through;
}
</style>
